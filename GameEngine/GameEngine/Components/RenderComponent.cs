using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Components
{
    public class RenderComponent : IComponent
    {
        public Texture2D Texture { get; set; }
    }
}
