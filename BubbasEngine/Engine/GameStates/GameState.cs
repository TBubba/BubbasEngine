using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.Content;
using BubbasEngine.Engine.Windows;
using BubbasEngine.Engine.Input;
using BubbasEngine.Engine.Graphics;
using BubbasEngine.Engine.Timing;
using BubbasEngine.Engine.Input.Devices;

namespace BubbasEngine.Engine.GameStates
{
    public abstract class GameState
    {
        // Private
        internal protected GameEngine _engine
        { get; private set; }
        protected GraphicsLayer _layer
        { get; private set; }
        protected KeyboardBindingCollection _keyboard
        { get; private set; }
        protected MouseBindingCollection _mouse
        { get; private set; }

        // Protected
        protected ContentManager _content
        { get { return _engine.Content; } }
        protected InputManager _input
        { get { return _engine.Input; } }
        protected GameStateManager _states
        { get { return _engine.States; } }
        protected GameWindow _window
        { get { return _engine.Window; } }
        protected GraphicsRenderer _graphics
        { get { return _engine.Graphics; } }
        protected TimeManager _time
        { get { return _engine.Time; } }

        // Constructor(s)
        public GameState()
        {
        }

        // Internal
        internal void Setup(GameEngine engine)
        {
            // Keep engine reference
            _engine = engine;

            // Create graphics layer
            _layer = _graphics.Layers.Create();

            // Create input binding collections
            _keyboard = _engine.Input.Keyboard.CreateGameBindigs(this);
            _mouse = _engine.Input.Mouse.CreateGameBindigs(this);
        }

        internal void OnRemoved()
        {
            // Remove graphics layer
            if (_graphics.Layers.Contains(_layer))
                _graphics.Layers.Remove(_layer);

            // Remove input binding collections

        }

        // Methods
        public abstract void Initialize();
        public abstract void LoadContent();
        public abstract void BeginFrame();
        public abstract void Step();
        public abstract void Animate(float delta);
        public abstract void UnloadContent();
    }
}
