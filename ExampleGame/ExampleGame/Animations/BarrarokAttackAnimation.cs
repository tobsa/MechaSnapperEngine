using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Framework;
using Microsoft.Xna.Framework;
using GameEngine.Components;

namespace ExampleGame.Animations
{
    public class BarrarokAttackAnimation : IAnimation
    {
        int frame = 4;
        int delay = 100;
        float elapsed;

        public void Update(GameTime gametime, Entity entity)
        {
            elapsed += (float)gametime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= delay)
            {
                if (frame > 8)
                {
                    frame = 4;
                }
                else
                {
                    frame++;
                }
                elapsed = 0;

                var renderComponent = ComponentManager.Instance.GetComponentOfType<RenderComponent>(entity);
                renderComponent.Frame = frame;
            }

        }
    }
}