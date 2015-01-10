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
        private SpriteFont Font;
        private CameraComponent CameraComponent;
        public GameOverState(MechaSnapperEngine engine) :
            base(engine)
        {
            Font = engine.Content.Load<SpriteFont>("Font");
            var cameraComponent = ComponentManager.Instance.GetComponentsOfType<CameraComponent>();
            CameraComponent = cameraComponent[0];
        }
        
        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime)
        {
            engine.SpriteBatch.DrawString(Font, "GAME OVER",
                   new Vector2(Matrix.Invert(CameraComponent.Transform).Translation.X + 550, Matrix.Invert(CameraComponent.Transform).Translation.Y + 360), Color.White);
        }
    }
}
