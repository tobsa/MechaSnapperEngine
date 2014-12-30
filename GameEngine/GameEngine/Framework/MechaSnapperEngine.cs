using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.Framework
{
    public class MechaSnapperEngine
    {
        private Game game;
        private GraphicsDeviceManager graphics;
        private GraphicsDevice graphicsDevice;
        private SpriteBatch spriteBatch;

        private GameStateManager stateManager = new GameStateManager();
        private SceneManager sceneManager = new SceneManager();

        public MechaSnapperEngine(Game game, int width, int height, bool fullscreen)
        {
            this.game = game;
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

        #region XNA Game Properties
        public ContentManager Content
        {
            get { return game.Content; }
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

        public Game Game
        {
            get { return game; }
        }
        #endregion

        public void RegisterState(GameState state)
        {
            stateManager.RegisterState(state);
        }

        public void UnregisterState(GameState state)
        {
            stateManager.UnregisterState(state);
        }

        public GameState State
        {
            get { return stateManager.State; }
        }

        public GameState GetState<T>()
        {
            return stateManager.GetRegisteredState<T>();
        }

        public void PushState<T>()
        {
            stateManager.PushState(stateManager.GetRegisteredState<T>());
        }

        public void PopState()
        {
            stateManager.PopState();
        }

        public void CreateWindow(int width, int height, bool fullscreen)
        {
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            graphics.IsFullScreen = fullscreen;
            graphics.ApplyChanges();
        }

        public void Update(GameTime gameTime)
        {
            var state = State;

            if(state.Enabled)
                state.Update(gameTime);

            InputManager.Instance.Update();
        }

        public void Draw(GameTime gameTime)
        {
            foreach (var state in stateManager.GameStates)
            {
                if(state.Visible)
                    
                    state.Draw(gameTime);
            }
        }
    }
}
