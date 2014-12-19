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
<<<<<<< HEAD
using ExampleGame.Systems;
using ExampleGame.Components;
=======
using ExampleGame.Animations;
>>>>>>> origin/master

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
            engine = new MechaSnapperEngine(this, 1600, 900, false);
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
            camComp = new CameraComponent(new Viewport());
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            Entity barrarok = EntityFactory.CreateEmptyEntity(EntityFactory.GenerateID, new Vector2(8 * 64, 10 * 64 + 8));
            Entity jack = EntityFactory.CreateEntity(EntityFactory.GenerateID, Content.Load<Texture2D>("UnluckyJack126"), new Vector2(2 * 64, 6 * 64));
<<<<<<< HEAD
            ComponentManager.Instance.AddComponent<RigidBodyComponent>(entity1, new RigidBodyComponent() { Friction = 0.01f, Gravity = 32});
<<<<<<< HEAD
           // ComponentManager.Instance.AddComponent<CameraComponent>(entity1, camComp);
=======
>>>>>>> origin/master
=======
>>>>>>> 35d3cba8e1b035d25402e20285efc96dab1b2013

            ComponentManager.Instance.AddComponent<AnimationComponent>(barrarok, new AnimationComponent(new BarrarokWalkingAnimation()));
            ComponentManager.Instance.AddComponent<RenderComponent>(barrarok, new RenderComponent(Content.Load<Texture2D>("BarrarokAnim"), 64, 124, 0));

            ComponentManager.Instance.AddComponent<InputComponent>(jack, new InputComponent(new JackInput()));
            ComponentManager.Instance.AddComponent<VelocityComponent>(jack, new VelocityComponent());
            ComponentManager.Instance.AddComponent<RigidBodyComponent>(jack, new RigidBodyComponent(28f, 0.3f, 0f));
            ComponentManager.Instance.AddComponent<CollisionRectangleComponent>(jack, new CollisionRectangleComponent(new Rectangle(2 * 64, 6 * 64, 64, 128)));

            var tiles = TileManager.LoadLevel(Content.Load<Texture2D>("Box64"));
            foreach (var tile in tiles)
                engine.SceneManager.AddEntity("Level1", 0, tile);

            engine.SceneManager.AddEntity("Level1", 0, background);
            engine.SceneManager.AddEntity("Level1", 1, barrarok);
            engine.SceneManager.AddEntity("Level1", 2, jack);
            engine.SceneManager.SetCurrentScene("Level1");

            InputManager.Instance.AddKeyBinding("Exit", Keys.Escape);
            InputManager.Instance.AddKeyBinding("Left", Keys.Left);
            InputManager.Instance.AddKeyBinding("Right", Keys.Right);
            InputManager.Instance.AddKeyBinding("Up", Keys.Up);
            InputManager.Instance.AddKeyBinding("Down", Keys.Down);
            InputManager.Instance.AddKeyBinding("Jump", Keys.Space);
            InputManager.Instance.AddKeyBinding("ChangeScene1", Keys.D1);
            InputManager.Instance.AddKeyBinding("ChangeScene2", Keys.D2);
            InputManager.Instance.AddKeyBinding("ChangeScene3", Keys.D3);
            InputManager.Instance.AddKeyBinding("ChangeScene1", Keys.NumPad1);
            InputManager.Instance.AddKeyBinding("ChangeScene2", Keys.NumPad2);
            InputManager.Instance.AddKeyBinding("ChangeScene3", Keys.NumPad3);

            var playingState = new PlayingState(engine);
            playingState.RegisterSystem(new RenderSystem(engine.SceneManager, engine.SpriteBatch));
            playingState.RegisterSystem(new InputSystem(engine.SceneManager));
            //playingState.RegisterSystem(new AISystem(engine.SceneManager));
            playingState.RegisterSystem(new PhysicsSystem(engine.SceneManager));
<<<<<<< HEAD
           // playingState.RegisterSystem(cameraSystem);
=======
            playingState.RegisterSystem(new AnimationSystem(engine.SceneManager, engine.SpriteBatch));
>>>>>>> origin/master

            engine.RegisterState(playingState);
            engine.RegisterState(new MainMenuState(engine));
            engine.RegisterState(new PausedState(engine));

            engine.PushState<MainMenuState>();
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
            cameraSystem.Update(gameTime); 
            base.Update(gameTime);
        }

        

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            engine.SpriteBatch.Begin();
           // engine.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, camComp.Transform);
            engine.Draw(gameTime);
            engine.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
