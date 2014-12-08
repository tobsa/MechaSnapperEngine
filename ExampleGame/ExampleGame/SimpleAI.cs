using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Framework;
using Microsoft.Xna.Framework;
using GameEngine.Components;

namespace ExampleGame
{
    public class SimpleAI : IAgentBehaviour
    {
        public void Update(GameTime gameTime, Entity entity)
        {
            var component = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entity);

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 move = component.Position;

            if (component.Position.X > 600)
            {
                move.X = 100;
            }
            else if (component.Position.X > 400)
            {
                //move.X -= 100 * dt;
            }

            move.X += 100 * dt;

            component.Position = move;
        }
    }
}
