using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Framework;
using Microsoft.Xna.Framework;
using GameEngine.Components;


namespace ExampleGame.Animations
{
    public class JackIdleAnimation : IAnimation
    {
        int frame = 0;
        int delay = 300;
        float elapsed;

        public void Update(GameTime gametime, Entity entity)
        {
            elapsed += (float)gametime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed > delay)
            {
                frame = 0;

                var renderComponent = ComponentManager.Instance.GetComponentOfType<RenderComponent>(entity);
                renderComponent.Frame = frame;
            }
        }
    }
}
