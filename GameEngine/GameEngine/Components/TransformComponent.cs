using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public class TransformComponent : IComponent
    {
        public TransformComponent() { }
        public TransformComponent(Vector2 position)
        {
            Position = position;
        }
        public Vector2 RotationOrigin { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }
    }
}
