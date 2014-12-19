using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using GameEngine.Framework;

namespace GameEngine.Components
{
    public class AnimationComponent : IComponent
    {
        public IAnimation Animation { set; get; }
    }
}
