﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameEngine.Framework;

namespace GameEngine.Components
{
    public class CollisionRectangleComponent : IComponent
    {
        public CollisionRectangleComponent() { }
        public CollisionRectangleComponent(Rectangle rectangle)
        {
            Rectangle = rectangle;
            Category = 0;
        }

        public Rectangle Rectangle { get; set; }
        public int Category { get; set; }
    }
}
