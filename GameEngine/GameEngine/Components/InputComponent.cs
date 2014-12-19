using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Framework;

namespace GameEngine.Components
{
    public class InputComponent : IComponent
    {
        public InputComponent() { }
        public InputComponent(IScript script)
        {
            Script = script;
        }

        public IScript Script { get; set; }
    }
}
