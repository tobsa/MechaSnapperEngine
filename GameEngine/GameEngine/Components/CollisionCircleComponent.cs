using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public class CollisionCircleComponent : Component
    {
        public Vector2 Center { get; set; }
        public float Radius { get; set; }
    }
}
