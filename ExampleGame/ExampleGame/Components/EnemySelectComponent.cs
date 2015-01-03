using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Components;
using Microsoft.Xna.Framework.Graphics;

namespace ExampleGame.Components
{
    public class EnemySelectComponent : IComponent
    {
        /*
         * Set to true if this object has been tagged with a button
         */
        public bool buttonTagged { get; set; }

        /*
         * Set to true if this object has been tagged by a player
         */
        public bool playerTagged { get; set; }

        /*
         * Button to be drawn
         */
        public Texture2D buttonTexture;

        /*
         * The button this component has been tagged with.
         * Empty it is not tagged
         */
        public string buttonName { get; set; }
    }
}
