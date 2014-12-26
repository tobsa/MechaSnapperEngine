﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Components;
using Microsoft.Xna.Framework.Graphics;

namespace ExampleGame.Components
{
    public class HealthComponent : IComponent
    {
        public Int16 CurrentHP { get; set; }
        public Int16 MaxHP { get; set; }
        public bool IsAlive { get; set; }
        public bool IsJack { get; set; }
    }
}