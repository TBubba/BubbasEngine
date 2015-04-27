using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.Input;
using BubbasEngine.Engine.Windows;
using BubbasEngine.Engine.Content;
using BubbasEngine.Engine.GameStates;
using BubbasEngine.Engine.Timing;
using BubbasEngine.Engine.Graphics;
using SFML.Graphics;
using BubbasEngine.Engine.Users;

namespace BubbasEngine.Engine
{
    public class GameEngine
    {
        // Private
        private ContentManager _content;
        private InputManager _input;
        private GraphicsRenderer _graphics;
        private GameStateManager _states;
        private TimeManager _time;
        private UserManager _users;
        private GameWindow _window;

        private GameArgs _args;

        private bool _isRunning;

        // Internal
        internal ContentManager Content
        { get { return _content; } }
        internal InputManager Input
        { get { return _input; } }
        internal GraphicsRenderer Graphics
        { get { return _graphics; } }
        internal GameStateManager States
        { get { return _states; } }
        internal TimeManager Time
        { get { return _time; } }
        internal UserManager Users
        { get { return _users; } }
        internal GameWindow Window
        { get { return _window; } }

        // Constructor(s)
        public GameEngine(GameArgs args)
        {
            // Copy arguments (to avoid outside editing of the arguments)
            _args = new GameArgs(args);

            // Console
            GameConsole.Initialize();

            // Content
            _content = new ContentManager(args.Content);

            // GameStates
            _states = new GameStateManager(this, args.States);

            // Graphics
            _graphics = new GraphicsRenderer();
            _graphics.SetRenderSize(args.Graphics.ResolutionWidth, args.Graphics.ResolutionHeight);

            // Input
            _input = new InputManager(args.Input);

            //
            _users = new UserManager(9001);

            // Time
            _time = new TimeManager(args.Time);

            // Window
            CreateWindow(args.Window);

            // Add Debug GameState
            if (args.Debug.Activated)
                _states.AddState(new Debugging.DebugGameState(args.Debug));
        }

        /// <summary>
        /// Starts the engine
        /// </summary>
        /// <param name="state">The state the engine will start running with. (Start-up state)</param>
        public void Run(GameState state)
        {
            // Throw exception if the game (engine) is already running (not sure if possible)
            if (_isRunning)
                throw new Exception("Engine is already running.");

            // Mark the game (engine) as running
            _isRunning = true;

            // Add the starup state
            if (state != null)
                _states.AddState(state);

            // Gameloop
            while (_isRunning)
                GameLoop();

            // Clear game components
            GameConsole.WriteLine("Cleaning up...");
            _states.Clear();

            // End
            GameConsole.WriteLine("Game ended.");
        }

        public void End()
        {
            // Mark the game (engine) as running
            _isRunning = false;
        }

        //
        private void CreateWindow(GameWindowArgs args)
        {
            // Create window
            _window = new GameWindow(args);

            //
            _input.SetWindow(_window);
            _graphics.SetRenderTarget(_window.Target);

            // End game when the window closes
            _window.Shut += delegate { End(); };
        }

        // GameLoop
        private void GameLoop()
        {
            // Timer
            _time.BeginFrame();

            // Begin Frame
            _states.BeginFrame();
            _input.BeginFrame();

            // Update Window
            _window.DispatchEvents();

            //
            _states.CallBeginFrame();

            // Update input
            _input.Update();

            // Step
            int steps = _time.GetStepCount();
            for (int i = 0; i < steps; i++)
                Step();

            // Draw
            Draw();
        }

        private void Step()
        {
            // States
            _states.CallStep();
        }

        private void Render(double frameTime)
        {
            // Animate
            _states.CallAnimate((float)frameTime);

            // Render
            _graphics.Render();
        }

        private void Draw()
        {
            // Clear the window (Getting ready to render)
            _window.Clear();

            // Render
            Render(_time.LastFrameTime());

            // Display waht ever is rendered
            _window.Display();
        }
    }
}
