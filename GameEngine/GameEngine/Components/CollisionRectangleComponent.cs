using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public class CollisionRectangleComponent : IComponent
    {
        public Rectangle Rectangle { get; set; }
    }
}
