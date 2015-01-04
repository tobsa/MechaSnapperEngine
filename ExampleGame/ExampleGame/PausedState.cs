using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ExampleGame.Components;
using GameEngine.Components;

namespace ExampleGame
{
    public class PausedState : GameState
    {
        SpriteFont font;
        public CameraComponent CameraComponent { get; set; }

        public PausedState(MechaSnapperEngine engine) :
            base(engine)
        {
            font = engine.Content.Load<SpriteFont>("Font");
            InputManager.Instance.AddKeyBinding("Paused", Microsoft.Xna.Framework.Input.Keys.Escape);
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.Instance.WasKeyDown("Paused"))
                engine.PopState();
        }

        public override void Draw(GameTime gameTime)
        {
            if(CameraComponent != null)
                engine.SpriteBatch.DrawString(font, "- Paused -",
                    new Vector2(Matrix.Invert(CameraComponent.Transform).Translation.X + 550, Matrix.Invert(CameraComponent.Transform).Translation.Y + 360), Color.White);
        }
    }
}
