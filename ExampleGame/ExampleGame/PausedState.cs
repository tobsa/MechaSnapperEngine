using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ExampleGame
{
    public class PausedState : GameState
    {
        SpriteFont font;

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
            engine.SpriteBatch.DrawString(font, "- Paused -", new Vector2(550, 350), Color.White);
        }
    }
}
