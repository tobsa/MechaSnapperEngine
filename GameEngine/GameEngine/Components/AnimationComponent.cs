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
        public Texture2D SpriteSheet { set; get; }
        public IAnimation Animation { set; get; }
        public int FrameWidth { set; get; }
        public int FrameHeight { set; get; }
    }
}
