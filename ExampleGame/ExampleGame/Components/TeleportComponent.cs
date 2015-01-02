using GameEngine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExampleGame.Components 
{
    class TeleportComponent : IComponent
    {
        public float Velocity { get; set; }
        public float Rotation { get; set; }
    }
}