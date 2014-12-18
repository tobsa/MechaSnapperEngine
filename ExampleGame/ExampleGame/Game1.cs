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

namespace ExampleGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        MechaSnapperEngine engine;

        public Game1()
        {
            engine = new MechaSnapperEngine(this, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height, true);
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
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            //Entity entity1 = EntityFactory.CreateEntity(0, Content.Load<Texture2D>("ship"), new Vector2(400, 300));
            //Entity entity2 = EntityFactory.CreateEntity(1, Content.Load<Texture2D>("ship2"), new Vector2(100, 100));
            //Entity entity3 = EntityFactory.CreateEntity(2, Content.Load<Texture2D>("ship"), new Vector2(600, 600));
            Entity background = EntityFactory.CreateEntity(-1, Content.Load<Texture2D>("Sky"), new Vector2(0,0));

            //AgentComponent agent = new AgentComponent();
            //agent.Behaviour = new SimpleAI();

            //ComponentManager.Instance.AddComponent<InputComponent>(entity1, new InputComponent());
            //ComponentManager.Instance.AddComponent<VelocityComponent>(entity1, new VelocityComponent() { Velocity = new Vector2(50, -200) });
            //ComponentManager.Instance.AddComponent<RigidBodyComponent>(entity1, new RigidBodyComponent() { Friction = 0.01f, Gravity = 32});
            //ComponentManager.Instance.AddComponent<AgentComponent>(entity3, agent);

            //engine.SceneManager.AddEntity("World1.Level1.Room1", -1, entity4);

            //engine.SceneManager.AddEntity("World1.Level1.Room1", 0, entity1);
            //engine.SceneManager.AddEntity("World1.Level1.Room1", 1, entity2);
            //engine.SceneManager.AddEntity("World1.Level1.Room1", 0, entity3);
            //engine.SceneManager.AddEntity("World1.Level1.Room2", 0, entity1);
            //engine.SceneManager.AddEntity("World1.Level2.Room1", 1, entity1);
            //engine.SceneManager.AddEntity("World1.Level2.Room1", 0, entity2);
            //engine.SceneManager.SetCurrentScene("World1.Level1.Room1");

            var entities = TileManager.LoadLevel(Content.Load<Texture2D>("Box64"));
            foreach (var entity in entities)
                engine.SceneManager.AddEntity("Level1", 1, entity);

            engine.SceneManager.AddEntity("Level1", 0, background);
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
            //playingState.RegisterSystem(new PhysicsSystem(engine.SceneManager));

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

            engine.SpriteBatch.Begin();
            engine.Draw(gameTime);
            engine.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
