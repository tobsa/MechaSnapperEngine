using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GameEngine.Framework;
using GameEngine.Systems;
using GameEngine.Components;
using ExampleGame.Systems;
using ExampleGame.Components;
using ExampleGame.Animations;
using ExampleGame.Scripts;
using ExampleGame.Enemies;

namespace ExampleGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        MechaSnapperEngine engine;
        CameraSystem cameraSystem;
        CameraComponent camComp;
        public Game1()
        {
            engine = new MechaSnapperEngine(this, 1280, 720, false);
            //engine = new MechaSnapperEngine(this, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height, true);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            engine.Initialize();

            cameraSystem = new CameraSystem(engine.SceneManager);
            camComp = new CameraComponent(GraphicsDevice.Viewport);
            camComp.XOffset = camComp.Viewport.Width / 2; //Make so that the camera follows the object in the middle of the screen

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            int[,] rocks = LoadRocks();

            int[,] rocksBG = LoadRocksBG();

            LoadEnemySelectSystem();
            
            List<Entity> rockEntities = EntityFactory.CreateTileWorld(rocks, Content.Load<Texture2D>("Rocks_FG_64x64"), 64, 64);
            List<Entity> rockBGEntities = EntityFactory.CreateTileWorld(rocksBG, Content.Load<Texture2D>("Rocks_BG_64x64"), 64, 64);

            Entity background = CreateBackground(new Vector2(350, 0));
            Entity barrarok = CreateBarrarok(new Vector2(10 * 64, 8 * 64));
            Entity barrarok2 = CreateBarrarok(new Vector2(16 * 64, 10 * 64));
            Entity jack = CreateJack(new Vector2(2 * 64, 4 * 80));
            Entity jackHealth = CreateJackHealth();
            Entity time = CreateTime(new Vector2(1200, 0), 500);

            Entity portalGun = EntityFactory.CreateEntity(EntityFactory.GenerateID, Content.Load<Texture2D>("PortalGun"), new Vector2(2 * 64, 4 * 64));
            Entity portalBullet = EntityFactory.CreateEntity(EntityFactory.GenerateID, Content.Load<Texture2D>("Projectile"), new Vector2(2 * 64, 4 * 64));
            portalBullet.Visible = false;

            ComponentManager.Instance.AddComponent(jack, new InputComponent(new JackInput(portalGun, portalBullet)));
            ComponentManager.Instance.AddComponent(portalGun, new ParentComponent(jack, 55, 70));
            //ComponentManager.Instance.AddComponent(portalGun, new InputComponent(new PortalScript()));

            ComponentManager.Instance.AddComponent(portalBullet, new TeleportComponent());

            FontManager.Instance.LoadFont("Font", Content.Load<SpriteFont>("Font"));
           

            //SoundManager.Instance.LoadSong("GameSong", Content.Load<Song>("Latin_Industries"));
           // SoundManager.Instance.PlaySong("GameSong"); //Spelar om när den är klar nu

            //engine.SceneManager.AddEntity("Level1", 0, background);
            //engine.SceneManager.AddEntity("Level1", 3, barrarok);
            //engine.SceneManager.AddEntity("Level1", 3, barrarok2);
            //engine.SceneManager.AddEntity("Level1", 3, jack);
            //engine.SceneManager.AddEntity("Level1", 3, jackHealth);
            //engine.SceneManager.AddEntity("Level1", 4, portalGun);
            //engine.SceneManager.AddEntity("Level1", 4, portalBullet);
            //engine.SceneManager.AddEntity("Level1", 5, time);
            //engine.SceneManager.AddEntities("Level1", 1, rockBGEntities);
            //engine.SceneManager.AddEntities("Level1", 2, rockEntities);

            engine.SceneManager.AddEntity("Level1", 0, background);
            engine.SceneManager.AddEntity("Level1", 3, barrarok);
            engine.SceneManager.AddEntity("Level1", 3, barrarok2);
            engine.SceneManager.AddEntity("Level1", 4, jack);
            engine.SceneManager.AddEntity("Level1", 4, jackHealth);
            engine.SceneManager.AddEntity("Level1", 4, portalGun);
            engine.SceneManager.AddEntity("Level1", 4, portalBullet);
            engine.SceneManager.AddEntity("Level1", 5, time);
            engine.SceneManager.AddEntities("Level1", 1, rockBGEntities);
            engine.SceneManager.AddEntities("Level1", 2, rockEntities);


            engine.SceneManager.SetCurrentScene("Level1");

            AddSoundEffects();
            AddKeyBindings();

            var playingState = new PlayingState(engine);
            playingState.RegisterSystem(new RenderSystem(engine.SceneManager, engine.SpriteBatch));
            playingState.RegisterSystem(new InputSystem(engine.SceneManager));
            playingState.RegisterSystem(new PhysicsSystem(engine.SceneManager));
            playingState.RegisterSystem(new AnimationSystem(engine.SceneManager, engine.SpriteBatch));
            playingState.RegisterSystem(new ParentSystem(engine.SceneManager));
            playingState.RegisterSystem(new TimeSystem(engine.SceneManager));
            playingState.RegisterSystem(cameraSystem);
            playingState.RegisterCamera(camComp);
            playingState.RegisterSystem(new AISystem(engine.SceneManager));
            playingState.RegisterSystem(enemySelectSystem);
            playingState.RegisterSystem(new HealthSystem(engine.SceneManager));

            var pausedState = new PausedState(engine);
            pausedState.CameraComponent = camComp;

            var mainMenuState = new MainMenuState(engine);
            mainMenuState.RegisterSystem(new RenderSystem(engine.SceneManager, engine.SpriteBatch));

            engine.RegisterState(playingState);
            engine.RegisterState(mainMenuState);
            engine.RegisterState(pausedState);

            engine.PushState<MainMenuState>();
        }

        private void AddKeyBindings()
        {
            InputManager.Instance.AddKeyBinding("Exit", Keys.Escape);
            InputManager.Instance.AddKeyBinding("Left", Keys.Left);
            InputManager.Instance.AddKeyBinding("Right", Keys.Right);
            InputManager.Instance.AddKeyBinding("Up", Keys.Up);
            InputManager.Instance.AddKeyBinding("Down", Keys.Down);
            InputManager.Instance.AddKeyBinding("Jump", Keys.Space);
            InputManager.Instance.AddKeyBinding("Shoot", Keys.F);
            InputManager.Instance.AddKeyBinding("RotateGunUp", Keys.Up);
            InputManager.Instance.AddKeyBinding("RotateGunDown", Keys.Down);

            InputManager.Instance.AddKeyBinding("Left2", Keys.A);
            InputManager.Instance.AddKeyBinding("Right2", Keys.D);

            InputManager.Instance.AddKeyBinding("ChangeScene1", Keys.D1);
            InputManager.Instance.AddKeyBinding("ChangeScene2", Keys.D2);
            InputManager.Instance.AddKeyBinding("ChangeScene3", Keys.D3);
            InputManager.Instance.AddKeyBinding("ChangeScene1", Keys.NumPad1);
            InputManager.Instance.AddKeyBinding("ChangeScene2", Keys.NumPad2);
            InputManager.Instance.AddKeyBinding("ChangeScene3", Keys.NumPad3);

            //Worldplayer change character
            InputManager.Instance.AddKeyBinding("LB", Microsoft.Xna.Framework.Input.Keys.P);
            InputManager.Instance.AddKeyBinding("LT", Microsoft.Xna.Framework.Input.Keys.O);
        }

        EnemySelectSystem enemySelectSystem;
        private void LoadEnemySelectSystem()
        {
            enemySelectSystem = new EnemySelectSystem(engine.SceneManager, engine.SpriteBatch);
            enemySelectSystem.AddButton("LB", Content.Load<Texture2D>("bumper_left"));
            enemySelectSystem.AddButton("LT", Content.Load<Texture2D>("trigger_left"));
            enemySelectSystem.AddButton("RB", Content.Load<Texture2D>("bumper_right"));
            enemySelectSystem.AddButton("RT", Content.Load<Texture2D>("trigger_right"));
        }

        private int[,] LoadRocks() {
            int[,] rocks = new int[,] 
            {
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 2, 1,-5,-5, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 1,-5,-5,-5, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 4,-5, 9, 8, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 2, 2, 0, 0, 8, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0},
                {0, 4,-5,-5, 6, 0, 0, 0, 0, 0, 0, 4,-5,-5,-5,-5,-5,-5, 6, 0, 0, 0, 0, 0, 0}, 
                {0, 0, 8, 8, 0, 2, 0, 0, 0, 0, 0, 0, 8, 8, 7,-5,-5, 9, 0, 2, 2, 2, 2, 2, 2}, 
                {0, 0, 0, 0, 4,-5, 6, 0, 0, 2, 2, 2, 2, 0, 0, 8, 8, 0, 4,-5,-5,-5,-5,-5,-5}, 
                {0, 0, 2, 2, 1,-5, 3, 2, 4,-5,-5,-5,-5, 6, 0, 0, 0, 0, 0, 7,-5,-5,-5, 9, 8}, 
                {0, 4,-5,-5,-5,-5,-5,-5, 6, 8, 8, 8, 8, 0, 0, 0, 0, 0, 0, 0, 7,-5, 9, 0, 0}, 
                {0, 0, 8, 8, 8, 8, 8, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 0, 0, 0}, 
                {-5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5, -5}, 
            };

            return rocks;
        }

        private int[,] LoadRocksBG() {
            int[,] rocksBG = new int[,] 
            {
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 5, 3, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 5, 5, 5, 3, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 6, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 1, 5, 5, 3, 2, 0, 0, 0, 0, 4, 5, 5, 5, 5, 5, 3, 0, 0},
                {0, 0, 0, 0, 0, 0, 4, 5, 5, 5, 5, 5, 6, 0, 0, 2, 1, 5, 5, 5, 5, 5, 5, 6, 0},
                {0, 0, 0, 0, 0, 4, 5, 5, 5, 5, 5, 5, 9, 2, 1, 5, 5, 5, 5, 5, 5, 5, 5, 6, 0},
                {2, 2, 2, 2, 2, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 3, 2},
                {5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5},
            };

            return rocksBG;
        }

        private Entity CreateBackground(Vector2 position)
        {
            Entity background = EntityFactory.CreateEntity(EntityFactory.GenerateID, Content.Load<Texture2D>("Sky"), position);

            ComponentManager.Instance.AddComponent(background, new IsFixedComponent(camComp));

            return background;
        }

        private Entity CreateTime(Vector2 position, int countDownSeconds)
        {
            Entity time = EntityFactory.CreateEmptyEntity(EntityFactory.GenerateID, position);

            ComponentManager.Instance.AddComponent(time, new StringRenderComponent());
            ComponentManager.Instance.AddComponent(time, new CountdownTimeComponent(countDownSeconds));
            ComponentManager.Instance.AddComponent(time, new IsFixedComponent(camComp));

            return time;
        }

        private Entity CreateJack(Vector2 position)
        {
            Entity jack = EntityFactory.CreateEmptyEntity(EntityFactory.GenerateID, position);

            ComponentManager.Instance.AddComponent(jack, new RenderComponent(Content.Load<Texture2D>("UnluckyJackAnim2"), 128, 128, 0));
            ComponentManager.Instance.AddComponent(jack, new AnimationComponent(new JackIdleAnimation()));
            ComponentManager.Instance.AddComponent(jack, new RigidBodyComponent(32f, 0.3f, 0f));
            ComponentManager.Instance.AddComponent(jack, new CollisionRectangleComponent(new Rectangle(2 * 64 + 32, 1 * 64, 64, 128)));
            ComponentManager.Instance.AddComponent(jack, new VelocityComponent());
            ComponentManager.Instance.AddComponent(jack, camComp);

            return jack;
        }

        private Entity CreateJackHealth()
        {
            Entity jackHealth = EntityFactory.CreateEmptyEntity(EntityFactory.GenerateID, Vector2.Zero);

            ComponentManager.Instance.AddComponent(jackHealth, new HealthComponent() { IsJack = true, IsAlive = true, CurrentHP = 3, MaxHP = 3 });
            ComponentManager.Instance.AddComponent(jackHealth, new RenderComponent(Content.Load<Texture2D>("hearts"), 144, 48, 0));
            ComponentManager.Instance.AddComponent(jackHealth, new IsFixedComponent(camComp));

            return jackHealth;
        }

        private Entity CreateBarrarok(Vector2 position)
        {
            Entity barrarok = EntityFactory.CreateEmptyEntity(EntityFactory.GenerateID, position);

            ComponentManager.Instance.AddComponent(barrarok, new AnimationComponent(new BarrarokWalkingAnimation()));
            ComponentManager.Instance.AddComponent(barrarok, new RenderComponent(Content.Load<Texture2D>("BarrarokAnim"), 64, 124, 0));
            ComponentManager.Instance.AddComponent(barrarok, new RigidBodyComponent(32f, 0.3f, 0f));
            ComponentManager.Instance.AddComponent(barrarok, new CollisionRectangleComponent(new Rectangle(0, 0, 32, 124)));
            ComponentManager.Instance.AddComponent(barrarok, new VelocityComponent());
            ComponentManager.Instance.AddComponent(barrarok, new AgentComponent() { Behaviour = new WalkingState(engine.SceneManager) });
            ComponentManager.Instance.AddComponent(barrarok, new EnemySelectComponent());

            return barrarok;
        }

        private void AddSoundEffects()
        {
            SoundManager.Instance.LoadSoundEffect("JackJump", Content.Load<SoundEffect>("JackJump"));
            SoundManager.Instance.LoadSoundEffect("JackDeath", Content.Load<SoundEffect>("JackDeath"));
            SoundManager.Instance.LoadSoundEffect("Punch", Content.Load<SoundEffect>("punch"));
            SoundManager.Instance.LoadSoundEffect("Punch2", Content.Load<SoundEffect>("punch2"));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            engine.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (camComp.IsRendering)
                engine.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,null, null, null, null, camComp.Transform);
            else
            engine.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            engine.Draw(gameTime);
            engine.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
