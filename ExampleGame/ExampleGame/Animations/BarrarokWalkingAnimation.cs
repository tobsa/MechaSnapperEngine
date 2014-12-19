using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Framework;
using Microsoft.Xna.Framework;

namespace ExampleGame.Animations
{
    public class BarrarokWalkingAnimation : IAnimation
    {
        int frame = 0;
        int delay = 300;
        float elapsed;

        public int GetCurrentFrame(GameTime gametime)
        {
            elapsed += (float)gametime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= delay)
            {
                if (frame > 2)
                {
                    frame = 0;
                }
                else
                {
                    frame++;
                }
                elapsed = 0;
            }

            return frame;
        }
    }
}
