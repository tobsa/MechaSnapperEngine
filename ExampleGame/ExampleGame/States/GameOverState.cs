using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Framework;
using Microsoft.Xna.Framework;
using ExampleGame.Components;
using Microsoft.Xna.Framework.Graphics;
using GameEngine.Components;

namespace ExampleGame.States
{
    class GameOverState : GameState
    {

        public CameraComponent CameraComponent { get; set; }
        public GameOverState(MechaSnapperEngine engine) :
            base(engine)
        {
        }
        
        public override void Update(GameTime gameTime)
        {
            if (InputManager.Instance.IsKeyDown(0, Microsoft.Xna.Framework.Input.Buttons.A, "Play"))
            {
                PlayingState playingState = (PlayingState)engine.GetState<PlayingState>();
                playingState.RestartCurrentLevel();
                engine.PopState();
            }
        }

        public override void Draw(GameTime gameTime)
        {

            engine.SpriteBatch.DrawString(FontManager.Instance.GetFont("Font"), "GAME OVER",
                   new Vector2(Matrix.Invert(CameraComponent.Transform).Translation.X + 550, CameraComponent.Viewport.Height / 4), Color.White);
            engine.SpriteBatch.DrawString(FontManager.Instance.GetFont("Font"), "PRESS ENTER / A TO RESTART",
                   new Vector2(Matrix.Invert(CameraComponent.Transform).Translation.X + 550, CameraComponent.Viewport.Height / 4 + FontManager.Instance.GetFont("Font").LineSpacing + 10), Color.White);
        }
    }
}
