using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Framework {
    public class FontManager {
        public static FontManager Instance {
            get {
                if (fontManager == null)
                    fontManager = new FontManager();

                return fontManager;
            }
        }
        public void LoadFont(string fontName, SpriteFont font){
            fonts.Add(fontName, font);
        }
        public SpriteFont GetFont(string fontName) {
#if DEBUG
            if(fonts[fontName] == null)
                Console.WriteLine("Font Error: Cant Find Font");
#endif
            return fonts[fontName];
        }
        private FontManager() { }
        private Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();
        private static FontManager fontManager;
    }
}
