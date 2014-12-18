using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Framework;
using Microsoft.Xna.Framework;

namespace ExampleGame.Animations
{
    public class BarrarokAttackAnimation : IAnimation
    {
        int frame = 4;
        int delay = 100;
        float elapsed;

        public int GetCurrentFrame(GameTime gametime)
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
            }

            return frame;
        }
    }
}