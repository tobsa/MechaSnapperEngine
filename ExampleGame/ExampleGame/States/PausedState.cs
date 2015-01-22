using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ExampleGame.Components;
using GameEngine.Components;
using Microsoft.Xna.Framework.Input;

namespace ExampleGame
{
    public class PausedState : GameState
    {
        public CameraComponent CameraComponent { get; set; }

        public PausedState(MechaSnapperEngine engine) :
            base(engine)
        {
            InputManager.Instance.AddKeyBinding("Paused", Microsoft.Xna.Framework.Input.Keys.Escape);
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.Instance.WasKeyDown(0, Buttons.Start,"Paused"))
                engine.PopState();
        }

        public override void Draw(GameTime gameTime)
        {
            if(CameraComponent != null)
                engine.SpriteBatch.DrawString(FontManager.Instance.GetFont("Font"), "- Paused -",
                    new Vector2(Matrix.Invert(CameraComponent.Transform).Translation.X + 550, Matrix.Invert(CameraComponent.Transform).Translation.Y + 360), Color.White);
        }
    }
}
