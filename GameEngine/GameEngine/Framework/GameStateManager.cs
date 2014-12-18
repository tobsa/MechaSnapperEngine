using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Framework
{
    public class GameStateManager
    {
        private Stack<GameState> states = new Stack<GameState>();
        private Dictionary<Type, GameState> registeredStates = new Dictionary<Type, GameState>();

        public event EventHandler OnStateChange;

        public void RegisterState(GameState state)
        {
            if (!registeredStates.ContainsKey(state.GetType()))
                registeredStates.Add(state.GetType(), state);
        }

        public void UnregisterState(GameState state)
        {
            registeredStates.Remove(state.GetType());
        }

        public GameState GetRegisteredState<T>()
        {
            return registeredStates[typeof(T)];
        }

        public List<GameState> GameStates
        {
            get 
            {
                var reversedStates = states.ToList();
                reversedStates.Reverse();
                return reversedStates;
            }
        }

        public void PushState(GameState state)
        {
            states.Push(state);

            OnStateChange += state.StateChanged;

            if (OnStateChange != null)
                OnStateChange(this, null);
        }

        public void PopState()
        {
            var state = states.Pop();

            OnStateChange -= state.StateChanged;

            if (OnStateChange != null)
                OnStateChange(this, null);
        }

        public GameState State
        {
            get { return states.Peek(); }
        }
    }
}
