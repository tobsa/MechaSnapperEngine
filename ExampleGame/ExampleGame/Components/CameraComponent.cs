using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ExampleGame.Components
{
    public class CameraComponent : IComponent
    {
        public bool IsRendering;
        public int depth;
        public Matrix Transform;
        public Viewport Viewport;
        public float Zoom = 1;
        public float XOffset;
        public float YOffset;

        public CameraComponent(Viewport viewPort)
        {
            Viewport = viewPort;
        }
    }
}
