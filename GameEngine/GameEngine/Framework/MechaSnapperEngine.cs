using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Framework
{
    public class MechaSnapperEngine
    {
        private GraphicsDeviceManager graphics;
        private GraphicsDevice graphicsDevice;
        private SpriteBatch spriteBatch;

        private SceneManager sceneManager = new SceneManager();
        private List<IUpdateableSystem> updateableSystems = new List<IUpdateableSystem>();
        private List<IRenderableSystem> renderableSystems = new List<IRenderableSystem>();

        public MechaSnapperEngine(Game game, int width, int height, bool fullscreen)
        {
            graphics = new GraphicsDeviceManager(game);
            CreateWindow(width, height, fullscreen);
            game.Content.RootDirectory = "Content";
        }

        public void Initialize()
        {
            graphicsDevice = graphics.GraphicsDevice;
            spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public SceneManager SceneManager
        {
            get { return sceneManager; }
        }

        public GraphicsDeviceManager GraphicsDeviceManager
        {
            get { return graphics; }
        }

        public GraphicsDevice GraphicsDevice
        {
            get { return graphicsDevice; }
        }

        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        public void CreateWindow(int width, int height, bool fullscreen)
        {
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            graphics.IsFullScreen = fullscreen;
            graphics.ApplyChanges();
        }

        public void RegisterSystem(EntitySystem system)
        {
            if (system is IUpdateableSystem)
                updateableSystems.Add(system as IUpdateableSystem);

            if (system is IRenderableSystem)
                renderableSystems.Add(system as IRenderableSystem);
        }

        public void UnregisterSystem(EntitySystem system)
        {
            if (system is IUpdateableSystem)
                updateableSystems.Remove(system as IUpdateableSystem);

            if (system is IRenderableSystem)
                renderableSystems.Remove(system as IRenderableSystem);
        }

        public virtual void Update(GameTime gameTime)
        {
            for (int i = 0; i < updateableSystems.Count; i++)
                updateableSystems[i].Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            for (int i = 0; i < renderableSystems.Count; i++)
                renderableSystems[i].Draw(gameTime);

            spriteBatch.End();
        }
    }
}
