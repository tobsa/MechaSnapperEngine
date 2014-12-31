using GameEngine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExampleGame.Components {
    class TeleportComponent : IComponent{
        public const int InGun = 0;
        public const int OnAir = 1;
        public const int OnFloor = 2;
        public TeleportComponent() { 
        }
        public int State { get; set; }
        public float Velocity { get; set; }
        public float VelocityX { get; set; }
        public float VelocityY { get; set; }
        public float ElevationAngle { get; set; }
    }
}