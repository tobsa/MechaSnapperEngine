using GameEngine.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExampleGame.Components {
    class TeleportComponent : IComponent{
        public static const int InChamber = 0;
        public static const int OnAir = 1;
        public static const int OnGround = 2;
        public int State { get; set; }
        public float ElevationDegree { get; set;}
        public float Velocity { get; set; }
        public float VelocityX { get; set; }
        public float VelocityY { get; set; }
    }
}
