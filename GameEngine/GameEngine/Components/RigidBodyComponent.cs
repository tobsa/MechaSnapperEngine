using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Components
{
    public class RigidBodyComponent : IComponent
    {
        public float Gravity { get; set; }
        public float Mass { get; set; }
    }
}
