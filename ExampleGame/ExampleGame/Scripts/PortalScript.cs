using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Framework;
using GameEngine.Components;
using Microsoft.Xna.Framework;

namespace ExampleGame.Scripts
{
    public class PortalScript : IScript
    {
        public void Update(GameTime gameTime, Entity entity)
        {
            var transform = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entity);

            if(InputManager.Instance.IsKeyDown("Up"))
            {
                if (transform.Rotation > - 0.8f)
                    transform.Rotation -= 0.1f;
            }
            if (InputManager.Instance.IsKeyDown("Down"))
            {
                if (transform.Rotation < 0.8f)
                transform.Rotation += 0.1f;
            }
        }
    }
}
