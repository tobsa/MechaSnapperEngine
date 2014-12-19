using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameEngine.Framework
{
    public interface IAnimation
    {
        int GetCurrentFrame(GameTime gametime);
    }
}
