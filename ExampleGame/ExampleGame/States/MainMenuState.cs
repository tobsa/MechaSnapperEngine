using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameEngine.Framework;

namespace ExampleGame
{
    public class MainMenuState : GameState
    {
        SpriteFont font;

        public MainMenuState(MechaSnapperEngine engine) :
            base (engine)
        {
            InputManager.Instance.AddKeyBinding("Play", Microsoft.Xna.Framework.Input.Keys.Enter);
            font = engine.Content.Load<SpriteFont>("Font");
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.Instance.WasKeyDown("Exit"))
                engine.Game.Exit();

            if (InputManager.Instance.WasKeyDown("Play"))
                engine.PushState<PlayingState>();
        }

        public override void Draw(GameTime gameTime)
        {
            engine.SpriteBatch.DrawString(font, "Main Menu\n Press Escape to quit game\n Press Enter to start playing", Vector2.Zero, Color.White);
        }
    }
}
