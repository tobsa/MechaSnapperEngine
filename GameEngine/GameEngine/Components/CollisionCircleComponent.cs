using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public class CollisionCircleComponent : IComponent
    {
        public CollisionCircleComponent() {}
        public CollisionCircleComponent(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        public Vector2 Center { get; set; }
        public float Radius { get; set; }
    }
}
