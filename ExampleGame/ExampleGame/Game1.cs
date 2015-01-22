using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GameEngine.Framework;
using GameEngine.Systems;
using GameEngine.Components;
using ExampleGame.Systems;
using ExampleGame.Components;
using ExampleGame.Animations;
using ExampleGame.Enemies;
using ExampleGame.States;

namespace ExampleGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        MechaSnapperEngine engine;
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

            camComp = new CameraComponent(GraphicsDevice.Viewport);
            camComp.XOffset = camComp.Viewport.Width / 4; //Make so that the camera follows the object in the middle of the screen

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            FontManager.Instance.LoadFont("Font", Content.Load<SpriteFont>("Font"));
           // SoundManager.Instance.LoadSong("GameSong", Content.Load<Song>("Latin_Industries"));
            //SoundManager.Instance.PlaySong("GameSong"); //Spelar om när den är klar nu

            AddSoundEffects();
            AddKeyBindings();

            var playingState = new PlayingState(engine);

            playingState.RegisterCamera(camComp);
            playingState.LoadLevels();

            var pausedState = new PausedState(engine);
            pausedState.CameraComponent = camComp;

            var mainMenuState = new MainMenuState(engine);
            mainMenuState.RegisterSystem(new RenderSystem(engine.SpriteBatch));


            engine.RegisterState(playingState);
            engine.RegisterState(mainMenuState);
            engine.RegisterState(pausedState);
            var gameOverState = new GameOverState(engine);
            gameOverState.CameraComponent = camComp;
            engine.RegisterState(gameOverState);
            var gameWonState = new GameWonState(engine);
            gameWonState.CameraComponent = camComp;
            engine.RegisterState(gameWonState);

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
            InputManager.Instance.AddKeyBinding("RB", Microsoft.Xna.Framework.Input.Keys.L);
            InputManager.Instance.AddKeyBinding("RT", Microsoft.Xna.Framework.Input.Keys.I);
        }

        private void AddSoundEffects()
        {
            SoundManager.Instance.LoadSoundEffect("JackJump", Content.Load<SoundEffect>("JackJump"));
            SoundManager.Instance.LoadSoundEffect("JackDeath", Content.Load<SoundEffect>("JackDeath"));
            SoundManager.Instance.LoadSoundEffect("Punch", Content.Load<SoundEffect>("punch"));
            SoundManager.Instance.LoadSoundEffect("Punch2", Content.Load<SoundEffect>("punch2"));
            SoundManager.Instance.LoadSoundEffect("Gunshot", Content.Load<SoundEffect>("gunshot"));
            SoundManager.Instance.LoadSoundEffect("Teleport", Content.Load<SoundEffect>("teleport"));
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
