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
            engine = new MechaSnapperEngine(this, 1200, 800, false);
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
            engine.RegisterSystem(new InputSystem(engine.SceneManager));
            engine.RegisterSystem(new RenderSystem(engine.SceneManager, engine.SpriteBatch));

            Entity entity1 = EntityFactory.CreateEntity(0, Content.Load<Texture2D>("ship"), new Vector2(400, 300));
            Entity entity2 = EntityFactory.CreateEntity(1, Content.Load<Texture2D>("ship2"), new Vector2(100, 100));

            ComponentManager.Instance.AddComponent<InputComponent>(entity1, new InputComponent());
            ComponentManager.Instance.AddComponent<VelocityComponent>(entity1, new VelocityComponent() { Velocity = new Vector2(200, 200) });

            engine.SceneManager.AddEntity("World1.Level1.Room1", 0, entity1);
            engine.SceneManager.AddEntity("World1.Level1.Room1", 1, entity2);

            engine.SceneManager.AddEntity("World1.Level1.Room2", 0, entity1);

            engine.SceneManager.AddEntity("World1.Level2.Room1", 1, entity1);
            engine.SceneManager.AddEntity("World1.Level2.Room1", 0, entity2);

            engine.SceneManager.SetCurrentScene("World1.Level1.Room1");

            InputManager.Instance.AddKeyBinding("Exit", Keys.Escape);
            InputManager.Instance.AddKeyBinding("Left", Keys.Left);
            InputManager.Instance.AddKeyBinding("Right", Keys.Right);
            InputManager.Instance.AddKeyBinding("Up", Keys.Up);
            InputManager.Instance.AddKeyBinding("Down", Keys.Down);

            InputManager.Instance.AddKeyBinding("ChangeScene1", Keys.D1);
            InputManager.Instance.AddKeyBinding("ChangeScene2", Keys.D2);
            InputManager.Instance.AddKeyBinding("ChangeScene3", Keys.D3);
            InputManager.Instance.AddKeyBinding("ChangeScene1", Keys.NumPad1);
            InputManager.Instance.AddKeyBinding("ChangeScene2", Keys.NumPad2);
            InputManager.Instance.AddKeyBinding("ChangeScene3", Keys.NumPad3);
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
            if (InputManager.Instance.IsKeyDown("Exit"))
                Exit();

            if (InputManager.Instance.IsKeyDown("ChangeScene1"))
                engine.SceneManager.SetCurrentScene("World1.Level1.Room1");
            if (InputManager.Instance.IsKeyDown("ChangeScene2"))
                engine.SceneManager.SetCurrentScene("World1.Level1.Room2");
            if (InputManager.Instance.IsKeyDown("ChangeScene3"))
                engine.SceneManager.SetCurrentScene("World1.Level2.Room1");

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

            engine.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
