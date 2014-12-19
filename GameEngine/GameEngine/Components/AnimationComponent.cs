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
        public AnimationComponent() { }
        public AnimationComponent(IAnimation animation)
        {
            Animation = animation;
        }

        public IAnimation Animation { set; get; }
    }
}
