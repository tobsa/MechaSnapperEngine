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

        private SpriteFont Font;
        private CameraComponent CameraComponent;
        public GameWonState(MechaSnapperEngine engine) :
            base(engine)
        {
            Font = engine.Content.Load<SpriteFont>("Font");
            var cameraComponent = ComponentManager.Instance.GetComponentsOfType<CameraComponent>();
            CameraComponent = cameraComponent[0];
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            engine.SpriteBatch.DrawString(Font, "YOU WON!",
                   new Vector2(Matrix.Invert(CameraComponent.Transform).Translation.X + 550, Matrix.Invert(CameraComponent.Transform).Translation.Y + 360), Color.White);
        }
    }
}
