using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameEngine.Components;
using Microsoft.Xna.Framework;

namespace ExampleGame.States
{
    public class GameWonState : GameState
    {
        public CameraComponent CameraComponent { get; set; }
        public GameWonState(MechaSnapperEngine engine) :
            base(engine)
        {
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (InputManager.Instance.IsKeyDown(0, Microsoft.Xna.Framework.Input.Buttons.A, "Play"))
            {
                PlayingState playingState = (PlayingState)engine.GetState<PlayingState>();
                playingState.NextLevel();
                engine.PopState();
            }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            engine.SpriteBatch.DrawString(FontManager.Instance.GetFont("Font"), "YOU WON!",
                   new Vector2(Matrix.Invert(CameraComponent.Transform).Translation.X + 550, Matrix.Invert(CameraComponent.Transform).Translation.Y + 360), Color.White);
            engine.SpriteBatch.DrawString(FontManager.Instance.GetFont("Font"), "PRESS ENTER / A TO PLAY NEXT LEVEL",
                   new Vector2(Matrix.Invert(CameraComponent.Transform).Translation.X + 550, CameraComponent.Viewport.Height / 4 + FontManager.Instance.GetFont("Font").LineSpacing + 10), Color.White);
        }
    }
}
