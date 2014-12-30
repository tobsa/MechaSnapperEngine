using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public class TransformComponent : IComponent
    {
        public TransformComponent() {
            Scale = new Vector2(1, 1);
        }
        public TransformComponent(Vector2 position)
        {
            Position = position;
            Scale = new Vector2(1,1);
        }
        public Vector2 RotationOrigin { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }
    }
}
