using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameEngine.Framework;
using ExampleGame.Components;
using GameEngine.Components;
using Microsoft.Xna.Framework.Input;
using ExampleGame.States;
using GameEngine.Systems;
using ExampleGame.Systems;
using ExampleGame.Animations;
using ExampleGame.Enemies;
using ExampleGame.Levels;

namespace ExampleGame
{
    public class PlayingState : GameState
    {
        private CameraComponent CameraComponent;
        private MoveSystem physicsSystem;
        private Dictionary<int, Level> Levels = new Dictionary<int, Level>();
        private Level CurrentLevel;
        private int CurrentLevelID;
        private HealthComponent HealthComponent;
        public PlayingState(MechaSnapperEngine engine) :
            base(engine)
        {
            InputManager.Instance.AddKeyBinding("MainMenu", Microsoft.Xna.Framework.Input.Keys.Escape);
            InputManager.Instance.AddKeyBinding("Paused", Microsoft.Xna.Framework.Input.Keys.Enter);
            InitializeSystems();
        }

        /*
         * Load levels and set first level as current scene.
         * Also set that levels walkable entities and Jack and the enemies o physicsSystem for enhanced performance
         */
        public void LoadLevels()
        {
            Levels.Add(1, new Level1(engine, this, CameraComponent));
            Levels.Add(2, new Level2(engine, this, CameraComponent));
            Levels[1].Initialize();
            CurrentLevel = Levels[1];
            CurrentLevelID = 1;
            SceneManager.Instance.SetCurrentScene(CurrentLevel.LevelName);

            physicsSystem.SetCollidableEntities(ComponentManager.Instance.GetEntities<CollisionRectangleComponent>(SceneManager.Instance.CurrentScene.Layers[Layers.WALKABLE_OBJECTS].Entities));
            var entities = ComponentManager.Instance.GetEntities<VelocityComponent>(SceneManager.Instance.CurrentScene.Layers[Layers.BARRAROK].Entities);
            entities.AddRange(ComponentManager.Instance.GetEntities<VelocityComponent>(SceneManager.Instance.CurrentScene.Layers[Layers.JACK].Entities));
            physicsSystem.SetJackAndEnemies(entities);
        }

        /*
         * Resets current level 
         */ 
        public void RestartCurrentLevel()
        {
            CurrentLevel.RestartLevel();
        }

        /*
         * Sets the new level.
         * Updates physicsSystem with the new walkable objects
         */
        public void NextLevel()
        {
            CurrentLevelID++;
            if (CurrentLevelID > 2)
            {
                engine.PushState<GameOverState>();
            }
            else
            {
                Levels[CurrentLevelID].Initialize();
                CurrentLevel = Levels[CurrentLevelID];
                SceneManager.Instance.SetCurrentScene(CurrentLevel.LevelName);
                physicsSystem.SetCollidableEntities(ComponentManager.Instance.GetEntities<CollisionRectangleComponent>(SceneManager.Instance.CurrentScene.Layers[Layers.WALKABLE_OBJECTS].Entities));
            }
        }

        /*
         * Initializes all the systems that the two levels are using
         */
        private void InitializeSystems()
        {
            
            RegisterSystem(new RenderSystem(engine.SpriteBatch));
            RegisterSystem(new InputSystem());
            physicsSystem = new MoveSystem();
            RegisterSystem(physicsSystem);
            RegisterSystem(new AnimationSystem(engine.SpriteBatch));
            RegisterSystem(new ParentSystem());
            RegisterSystem(new TimeSystem());
            RegisterSystem(new CameraSystem());
            RegisterCamera(CameraComponent);
            RegisterSystem(new AISystem());

            EnemySelectSystem enemySelectSystem = new EnemySelectSystem(engine.SpriteBatch);
            enemySelectSystem.AddButton("LB", engine.Content.Load<Texture2D>("bumper_left"));
            enemySelectSystem.AddButton("LT", engine.Content.Load<Texture2D>("trigger_left"));
            enemySelectSystem.AddButton("RB", engine.Content.Load<Texture2D>("bumper_right"));
            enemySelectSystem.AddButton("RT", engine.Content.Load<Texture2D>("trigger_right"));

            RegisterSystem(enemySelectSystem);
            RegisterSystem(new HealthSystem());
            RegisterSystem(new PortalSystem());
        }


        public override void Update(GameTime gameTime)
        {
            SetCameraToRendering();

            if (InputManager.Instance.WasKeyDown(0, Buttons.Start, "MainMenu"))
            {
                SetCameraToNotRendering();
                engine.PopState();
            }
            else if (InputManager.Instance.WasKeyDown(0, Buttons.Start,"Paused"))
            {
                engine.PushState<PausedState>();
            }

            if (HealthComponent == null)
            {
                var healthComponent = ComponentManager.Instance.GetComponentsOfType<HealthComponent>();
                HealthComponent = healthComponent[0];
            }
            if (HealthComponent.CurrentHP == 0)
            {
                engine.PushState<GameOverState>();
            }
            if (HealthComponent.HasHorseShoe)
            {
                engine.PushState<GameWonState>();
            }

            for (int i = 0; i < updateableSystems.Count; i++)
                updateableSystems[i].Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

                for (int i = 0; i < renderableSystems.Count; i++)
                    renderableSystems[i].Draw(gameTime);

                engine.SpriteBatch.DrawString(FontManager.Instance.GetFont("Font"), "Playing\n Press Escape to go back to main menu\n Press Enter to pause/unpause the game", Vector2.Zero, Color.White);
        }

        public override void StateChanged(object sender, EventArgs e)
        {
            base.StateChanged(sender, e);
            
            if (engine.State == engine.GetState<PausedState>() || engine.State == engine.GetState<GameOverState>())
            {
                Visible = true;
            }
        }

        private void SetCameraToNotRendering()
        {
            if (CameraComponent != null)
                CameraComponent.IsRendering = false;
        }

        private void SetCameraToRendering()
        {
            if (CameraComponent != null)
                CameraComponent.IsRendering = true;
        }

        private bool IsCameraRendering()
        {
            if (CameraComponent != null)
                return CameraComponent.IsRendering;
            return false;
        }

        public void RegisterCamera(CameraComponent camera)
        {
            CameraComponent = camera;
        }

        
    }
}
