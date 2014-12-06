using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Framework
{
    public class InputManager
    {
        private Dictionary<string, List<Keys>> keyBindings = new Dictionary<string, List<Keys>>();
        private KeyboardState prevKeyboardState;
        private KeyboardState newKeyboardState;

        private static InputManager inputManager;

        private InputManager() { }

        public static InputManager Instance
        {
            get
            {
                if (inputManager == null)
                    inputManager = new InputManager();

                return inputManager;
            }
        }

        public void AddKeyBinding(string action, Keys value)
        {
            if (!keyBindings.ContainsKey(action))
                keyBindings[action] = new List<Keys>();

            keyBindings[action].Add(value);
        }

        public void RemoveKeyBinding(string key, Keys value)
        {
            keyBindings[key].Remove(value);
        }

        public bool IsKeyDown(string action)
        {
            foreach (var key in keyBindings[action])
                if (newKeyboardState.IsKeyDown(key))
                    return true;

            return false;
        }

        public bool WasKeyDown(string action)
        {
            foreach (var key in keyBindings[action])
            {
                if (newKeyboardState.IsKeyDown(key) && prevKeyboardState.IsKeyUp(key))
                    return true;
            }

            return false;
        }

        public void Update()
        {
            prevKeyboardState = newKeyboardState;
            newKeyboardState = Keyboard.GetState();
        }
    }
}
