using GameEngine.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExampleGame.Components {
    class TeleportComponent : IComponent{
        public const int InChamber = 0;
        public const int OnAir = 1;
        public const int OnGround = 2;
        public int State { get; set; }
        public float ElevationDegree { get; set;}
        public float Velocity { get; set; }
        public float VelocityX { get; set; }
        public float VelocityY { get; set; }
    }
}
