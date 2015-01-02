using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Components {
    public class StringRenderComponent : IComponent{
        public StringRenderComponent() {
            Text = "";
            Font = "Font";
            Effect = SpriteEffects.None;
        }
        public string Text { get; set; }
        public string Font { get; set; }
        public SpriteEffects Effect { get; set; }
    }
}
