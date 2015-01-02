using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Components
{
    public class TransformComponent : IComponent
    {

        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }
        public Vector2 Origin { set; get; }

        public TransformComponent() { }
        public TransformComponent(Vector2 position)
        {
            Position = position;
        }


    }
}
