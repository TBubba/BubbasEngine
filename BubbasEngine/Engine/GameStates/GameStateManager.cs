using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.Content;

namespace BubbasEngine.Engine.GameStates
{
    public class GameStateManager
    {
        // Private
        private GameEngine _engine;
        private Dictionary<GameState, GameStateCondition> _states;

        private GameStateCondition _defaultCondition;

        private Action _beginFrame;

        private Action _stateBeginFrame;
        private Action _stateStep;
        private Action<float> _stateAnimate;
        
        // Constructor(s)
        internal GameStateManager(GameEngine engine, GameStateManagerArgs args)
        {
            //
            _defaultCondition = new GameStateCondition(args.DefaultCondition);

            //
            _states = new Dictionary<GameState, GameStateCondition>();

            //
            _engine = engine;
        }

        // States
        public void AddState(GameState state)
        {
            AddState(state, _defaultCondition);
        }
        public void AddState(GameState state, GameStateCondition con)
        {
            // Cant add the same state multiple times
            if (_states.ContainsKey(state))
                return;

            // Add state
            _states.Add(state, new GameStateCondition());

            // Set up state
            state.Setup(_engine);

            // Initialize state
            state.Initialize();

            // Load resources
            LoadState(state);

            // Enable custom settings
            if (con.BeginFrame) // BeginFrame
                EnableBeginFrame(state);
            if (con.Step) // Step
                EnableStep(state);
            if (con.Draw) // Draw
                EnableDraw(state);

            // Debug
            GameConsole.WriteLine(string.Format("GameState Added: \"{0}\"", state.GetType().ToString()), GameConsole.MessageType.Important);
        }

        public bool RemoveState(GameState state)
        {
            // If the state exists
            if (_states.ContainsKey(state))
            {
                // Disable state
                GameStateCondition con = _states[state];
                if (con.BeginFrame) // BeginFrame
                    DisableBeginFrame(state);
                if (con.Step) // Step
                    DisableStep(state);
                if (con.Draw) // Draw
                    DisableDraw(state);

                // Unload
                UnloadContent(state);

                // Remove the state
                _beginFrame += () => { _states.Remove(state); state.OnRemoved(); };

                // Debug
                GameConsole.WriteLine(string.Format("GameState Removed: \"{0}\"", state.GetType().ToString()));

                // State found and removed (Success)
                return true;
            }

            // State not found (Fail)
            GameConsole.WriteLine(string.Format("{0}: Tried to remove GameState \"{1}\"", this.GetType().Name, state.GetType().ToString()), GameConsole.MessageType.Important); // Debug
            return false;
        }

        // 
        private void LoadState(GameState state)
        {
            // Add to dele
            _beginFrame += () =>
                {
                    state.LoadContent();
                };
        }
        private void UnloadContent(GameState state)
        {
            // Add to dele
            _beginFrame += () => { state.UnloadContent(); };
        }

        // Enable
        internal void EnableBeginFrame(GameState state)
        {
            //if (_states.ContainsKey(state))
            if (_states[state].BeginFrame != true)
            {
                // Update flag
                _states[state].BeginFrame = true;

                // Add to dele
                _beginFrame += () => { _stateBeginFrame += state.BeginFrame; };
            }
        }
        internal void EnableStep(GameState state)
        {
            //if (_states.ContainsKey(state))
            if (_states[state].Step != true)
            {
                // Update flag
                _states[state].Step = true;

                // Add to dele
                _beginFrame += () => { _stateStep += state.Step; };
            }
        }
        private void EnableDraw(GameState state)
        {
            //if (_states.ContainsKey(state))
            if (_states[state].Draw != true)
            {
                // Update flag
                _states[state].Draw = true;

                // Add to dele
                _beginFrame += () => { _stateAnimate += state.Animate; };
            }
        }

        // Disable
        private void DisableBeginFrame(GameState state)
        {
            //if (_states.ContainsKey(state))
            if (_states[state].BeginFrame != false)
            {
                // Update flag
                _states[state].BeginFrame = false;

                // Add to dele
                _beginFrame += () => { _stateBeginFrame -= state.BeginFrame; };
            }
        }
        private void DisableStep(GameState state)
        {
            //if (_states.ContainsKey(state))
            if (_states[state].Step != false)
            {
                // Update flag
                _states[state].Step = false;

                // Add to dele
                _beginFrame += () => { _stateStep -= state.Step; };
            }
        }
        private void DisableDraw(GameState state)
        {
            //if (_states.ContainsKey(state))
            if (_states[state].Draw != false)
            {
                // Update flag
                _states[state].Draw = false;

                // Add to dele
                _beginFrame += () => { _stateAnimate -= state.Animate; };
            }
        }

        //
        internal void BeginFrame()
        {
            if (_beginFrame != null)
            {
                _beginFrame();
                _beginFrame = null;
            }
        }

        //
        internal void CallBeginFrame()
        {
            if (_stateBeginFrame != null)
                _stateBeginFrame();
        }
        internal void CallStep()
        {
            if (_stateStep != null)
                _stateStep();
        }
        internal void CallAnimate(float delta)
        {
            if (_stateAnimate != null)
                _stateAnimate(delta);
        }

        // Clear
        internal void Clear()
        {
            // Remove all states
            int length = _states.Count;
            foreach (KeyValuePair<GameState, GameStateCondition> pair in _states)
                RemoveState(pair.Key);

            // Unloads the states
            if (_beginFrame != null)
                _beginFrame();
        }
    }
}
