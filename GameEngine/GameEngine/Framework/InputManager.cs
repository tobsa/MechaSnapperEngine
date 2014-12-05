using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Framework
{
    public class InputManager
    {
        private static InputManager inputManager;
        private Dictionary<string, List<Keys>> keyBindings = new Dictionary<string, List<Keys>>();

        public static InputManager Instance
        {
            get
            {
                if (inputManager == null)
                    inputManager = new InputManager();

                return inputManager;
            }
        }

        public void AddKeyBinding(string action, Keys key)
        {
            if (!keyBindings.ContainsKey(action))
                keyBindings.Add(action, new List<Keys>());

            keyBindings[action].Add(key);
        }

        public void RemoveKeyBinding(string action, Keys key)
        {
            if (keyBindings.ContainsKey(action))
                keyBindings[action].Remove(key);
        }

        public bool IsKeyDown(string action)
        {
            KeyboardState state = Keyboard.GetState();

            var keys = keyBindings[action];
            for (int i = 0; i < keys.Count; i++)
                if (state.IsKeyDown(keys[i]))
                    return true;

            return false;
        }
    }
}
