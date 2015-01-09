using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace GameEngine.Framework
{
    public class InputManager
    {
        private Dictionary<string, List<Keys>> keyBindings = new Dictionary<string, List<Keys>>();
        private KeyboardState prevKeyboardState;
        private KeyboardState newKeyboardState;
        private GamePadState[] prevGamePadsState = new GamePadState[2];
        private GamePadState[] gamePadsState = new GamePadState[2];
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

        public bool IsKeyDown(int playerIndex, Buttons button, string action)
        {
            foreach (var key in keyBindings[action])
                if (newKeyboardState.IsKeyDown(key))
                    return true;
            
            return (gamePadsState[playerIndex].IsButtonDown(button));
        }


        public bool WasKeyDown(int playerIndex, Buttons button, string action)
        {

            foreach (var key in keyBindings[action])
            {
                if (newKeyboardState.IsKeyDown(key) && prevKeyboardState.IsKeyUp(key))
                    return true;
            }

            return (gamePadsState[playerIndex].IsButtonDown(button) && prevGamePadsState[playerIndex].IsButtonUp(button));

        }

        public void Update()
        {
            prevKeyboardState = newKeyboardState;
            newKeyboardState = Keyboard.GetState();

            //set our previous state to our new state
            prevGamePadsState[0] = gamePadsState[0];
            prevGamePadsState[1] = gamePadsState[1];

            //get our new state
            //gamePadsState = GamePad.State .GetState();
            gamePadsState[0] = GamePad.GetState(PlayerIndex.One);
            gamePadsState[1] = GamePad.GetState(PlayerIndex.Two);
        }
    }
}
