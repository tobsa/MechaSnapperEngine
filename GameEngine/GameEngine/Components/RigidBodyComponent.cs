using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Components
{
    public class RigidBodyComponent : IComponent
    {
        public RigidBodyComponent() { }
        public RigidBodyComponent(float gravity, float friction, float mass)
        {
            Gravity = gravity;
            Friction = friction;
            Mass = mass;
            OnGround = false;
        }

        public float Gravity { get; set; }
        public float Friction { get; set; }
        public float Mass { get; set; }
        public bool OnGround { get; set; }
    }
}
