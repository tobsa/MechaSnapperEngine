using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Components
{
    public class RenderComponent : IComponent
    {
        public Texture2D Texture { get; set; }
        public SpriteEffects SpriteEffect;
        public int Width { set; get; }
        public int Height { set; get; }
        public int Frame { set; get; }

        public RenderComponent(Texture2D Texture)
        {
            this.Texture = Texture;
            this.Width = Texture.Width;
            this.Height = Texture.Width;
            this.Frame = 0;
        }

        // Constructor for Animation
        public RenderComponent(Texture2D Texture, int Width, int Height, int Frame)
        {
            this.Texture = Texture;
            this.Width = Width;
            this.Height = Height;
            this.Frame = Frame;
        }
    }
}
