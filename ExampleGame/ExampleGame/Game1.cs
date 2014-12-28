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
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, 
            };

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
            

            List<Entity> rockEntities = EntityFactory.CreateTileWorld(rocks, Content.Load<Texture2D>("Rocks_FG_64x64"), 64, 64);
            List<Entity> rockBGEntities = EntityFactory.CreateTileWorld(rocksBG, Content.Load<Texture2D>("Rocks_BG_64x64"), 64, 64);

            Entity background = EntityFactory.CreateEntity(EntityFactory.GenerateID, Content.Load<Texture2D>("Sky"), new Vector2(0, 0));
            Entity barrarok = EntityFactory.CreateEmptyEntity(EntityFactory.GenerateID, new Vector2(10 * 64, 8 * 64 + 8));
            Entity jack = EntityFactory.CreateEmptyEntity(EntityFactory.GenerateID, new Vector2(2 * 64, 4 * 64));

            ComponentManager.Instance.AddComponent(barrarok, new AnimationComponent(new BarrarokWalkingAnimation()));
            ComponentManager.Instance.AddComponent(barrarok, new RenderComponent(Content.Load<Texture2D>("BarrarokAnim"), 64, 124, 0));
            
            ComponentManager.Instance.AddComponent(jack, new RenderComponent(Content.Load<Texture2D>("UnluckyJackAnim"), 128, 128, 0));
            ComponentManager.Instance.AddComponent(jack, new AnimationComponent(new JackIdleAnimation()));
            ComponentManager.Instance.AddComponent(jack, new RigidBodyComponent(32f, 0.3f, 0f));
            ComponentManager.Instance.AddComponent(jack, new CollisionRectangleComponent(new Rectangle(2 * 64 + 32, 1 * 64, 64, 128)));
            ComponentManager.Instance.AddComponent(jack, new VelocityComponent());
            ComponentManager.Instance.AddComponent(jack, new InputComponent(new JackInput()));

            //ComponentManager.Instance.AddComponent(jack, camComp);

            engine.SceneManager.AddEntity("Level1", 0, background);
            engine.SceneManager.AddEntity("Level1", 3, barrarok);
            engine.SceneManager.AddEntity("Level1", 3, jack);

            engine.SceneManager.AddEntities("Level1", 1, rockBGEntities);
            engine.SceneManager.AddEntities("Level1", 2, rockEntities);

            engine.SceneManager.SetCurrentScene("Level1");

            engine.SceneManager.SetCurrentScene("Level1");

            InputManager.Instance.AddKeyBinding("Exit", Keys.Escape);
            InputManager.Instance.AddKeyBinding("Left", Keys.Left);
            InputManager.Instance.AddKeyBinding("Right", Keys.Right);
            InputManager.Instance.AddKeyBinding("Up", Keys.Up);
            InputManager.Instance.AddKeyBinding("Down", Keys.Down);
            InputManager.Instance.AddKeyBinding("Jump", Keys.Space);
            InputManager.Instance.AddKeyBinding("Shoot", Keys.F);

            InputManager.Instance.AddKeyBinding("ChangeScene1", Keys.D1);
            InputManager.Instance.AddKeyBinding("ChangeScene2", Keys.D2);
            InputManager.Instance.AddKeyBinding("ChangeScene3", Keys.D3);
            InputManager.Instance.AddKeyBinding("ChangeScene1", Keys.NumPad1);
            InputManager.Instance.AddKeyBinding("ChangeScene2", Keys.NumPad2);
            InputManager.Instance.AddKeyBinding("ChangeScene3", Keys.NumPad3);

            var playingState = new PlayingState(engine);
            playingState.RegisterSystem(new RenderSystem(engine.SceneManager, engine.SpriteBatch));
            playingState.RegisterSystem(new InputSystem(engine.SceneManager));
            playingState.RegisterSystem(new PhysicsSystem(engine.SceneManager));
            playingState.RegisterSystem(new AnimationSystem(engine.SceneManager, engine.SpriteBatch));
            //playingState.RegisterSystem(cameraSystem);
            //playingState.RegisterCamera(camComp);
            var mainMenuState = new MainMenuState(engine);
            mainMenuState.RegisterSystem(new RenderSystem(engine.SceneManager, engine.SpriteBatch));

            engine.RegisterState(playingState);
            engine.RegisterState(mainMenuState);
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
            base.Update(gameTime);
        }

        

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //engine.SpriteBatch.Begin();
            //if (camComp.IsRendering)
            //    engine.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,null, null, null, null, camComp.Transform);
            //else
                engine.SpriteBatch.Begin();

            engine.Draw(gameTime);
            engine.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
