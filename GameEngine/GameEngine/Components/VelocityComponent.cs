using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public class VelocityComponent : IComponent
    {
        public VelocityComponent() { }
        public VelocityComponent(Vector2 velocity)
        {
            Velocity = velocity;
        }

        public Vector2 Velocity { get; set; }
    }
}
