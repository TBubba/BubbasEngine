using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using BubbasEngine.Engine.GameStates;
using BubbasEngine.Engine.Generic;
using BubbasEngine.Engine.Windows;

namespace BubbasEngine.Engine.Input.Devices
{
    // Event args
    public class MouseWheelEventArgs
    {
        public int X;
        public int Y;
        public int Delta;

        public MouseWheelEventArgs()
        {
        }
        internal MouseWheelEventArgs(SFMLMouseWheelEventArgs args)
        {
            X = args.X;
            Y = args.Y;
            Delta = args.Delta;
        }
        public MouseWheelEventArgs(int x, int y, int delta)
        {
            X = x;
            Y = y;
            Delta = delta;
        }
    }
    public class MouseMoveEventArgs
    {
        public int X;
        public int Y;

        public MouseMoveEventArgs()
        {
        }
        internal MouseMoveEventArgs(SFMLMouseMoveEventArgs args)
        {
            X = args.X;
            Y = args.Y;
        }
        public MouseMoveEventArgs(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    public class MouseButtonEventArgs
    {
        public int X;
        public int Y;
        public Mouse.Button Button;

        public MouseButtonEventArgs()
        {
        }
        internal MouseButtonEventArgs(SFMLMouseButtonEventArgs args)
        {
            X = args.X;
            Y = args.Y;
            Button = (Mouse.Button)(int)args.Button;
        }
        public MouseButtonEventArgs(int x, int y, Mouse.Button button)
        {
            X = x;
            Y = y;
            Button = button;
        }
    }

    public class Mouse
    {
        /// <summary>
        /// Mouse buttons
        /// </summary>
        public enum Button
        {
            /// <summary>The left mouse button</summary>
            Left,

            /// <summary>The right mouse button</summary>
            Right,

            /// <summary>The middle (wheel) mouse button</summary>
            Middle,

            /// <summary>The first extra mouse button</summary>
            XButton1,

            /// <summary>The second extra mouse button</summary>
            XButton2,

            /// <summary>Keep last -- the total number of mouse buttons</summary>
            ButtonCount
        }

        // Private
        private bool[] _isbuttonDown; // Button states (true = down)
        private bool[] _oldButtonDown; // Old button states (true = down)

        private int _x; // Mouse position
        private int _y;
        private int _oldX; // Old mouse position (last update / tick)
        private int _oldY;
        private int _prevX; // Old mouse position (last move)
        private int _prevY;

        private MouseBindingCollection _bindings; // Directly bound input (through the input class)
        private Dictionary<GameState, MouseBindingCollection> _gameBindings; // Input bound through game states

        private Action _beginFrame; // Functions/Methods that will be called on next "BeginFrame"
        private Action _update; // Functions/Methods that will be called on next update

        private GameWindow _window;

        // Public
        public MouseBindingCollection Bindings
        { get { return _bindings; } }

        /// <summary>
        /// Current X position
        /// </summary>
        public int X
        { get { return _x; } }
        /// <summary>
        /// Current Y position
        /// </summary>
        public int Y
        { get { return _y; } }

        /// <summary>
        /// X position from last update
        /// </summary>
        public int OldX
        { get { return _oldX; } }
        /// <summary>
        /// Y position from last update
        /// </summary>
        public int OldY
        { get { return _oldY; } }

        /// <summary>
        /// X position before previous move
        /// </summary>
        public int PrevX
        { get { return _prevX; } }
        /// <summary>
        /// Y position before previous move
        /// </summary>
        public int PrevY
        { get { return _prevY; } }

        // Constructor(s)
        internal Mouse()
        {
            // Create button register
            _isbuttonDown = new bool[(int)Mouse.Button.ButtonCount];
            _oldButtonDown = new bool[(int)Mouse.Button.ButtonCount];

            // Create keybinding containers
            _bindings = new MouseBindingCollection();
            _gameBindings = new Dictionary<GameState, MouseBindingCollection>();
        }

        // Gameloop
        internal void BeginFrame()
        {
            // Call methods
            if (_beginFrame != null)
            {
                _beginFrame();
                _beginFrame = null;
            }
        }
        internal void Update(bool focus)
        {
            // Push button register
            PushButtonRegister();

            // Push mouse position
            PushMousePosition();

            //
            if (focus)
            {
                if (_update != null)
                {
                    _update();
                    _update = null;
                }
            }
        }

        // GameState collection handling
        internal MouseBindingCollection CreateGameStateBindigs(GameState gs)
        {
            // Create new bindings collection (for that game state)
            MouseBindingCollection collection = new MouseBindingCollection();
            _gameBindings[gs] = collection;

            // Return collection
            return collection;
        }
        internal void RemoveGameBindings(GameState gs)
        {
            // Remove bindings collection
            _gameBindings.Remove(gs);
        }

        // Add (to events)
        public void AddOnPressed(Button button, DeleHandler<MouseButtonEventArgs> bind)
        {
            // Add at the beginning of the next frame
            _beginFrame += delegate { _bindings.AddOnPressed(button, bind); };
        }
        public void AddOnReleased(Button button, DeleHandler<MouseButtonEventArgs> bind)
        {
            // Add at the beginning of the next frame
            _beginFrame += delegate { _bindings.AddOnReleased(button, bind); };
        }
        public void AddOnWheelChanged(DeleHandler<MouseWheelEventArgs> bind)
        {
            // Add at the beginning of the next frame
            _beginFrame += delegate { _bindings.AddOnWheelChanged(bind); };
        }
        public void AddOnMoved(DeleHandler<MouseMoveEventArgs> bind)
        {
            // Add at the beginning of the next frame
            _beginFrame += delegate { _bindings.AddOnMoved(bind); };
        }

        // Remove (from events)
        public void RemoveOnPressed(Button button, DeleHandler<MouseButtonEventArgs> bind)
        {
            // Remove at the beginning of the next frame
            _beginFrame += delegate { _bindings.RemoveOnPressed(button, bind); };
        }
        public void RemoveOnReleased(Button button, DeleHandler<MouseButtonEventArgs> bind)
        {
            // Remove at the beginning of the next frame
            _beginFrame += delegate { _bindings.RemoveOnReleased(button, bind); };
        }
        public void RemoveOnWheelChanged(DeleHandler<MouseWheelEventArgs> bind)
        {
            // Remove at the beginning of the next frame
            _beginFrame += delegate { _bindings.RemoveOnWheelChanged(bind); };
        }
        public void RemoveOnMoved(DeleHandler<MouseMoveEventArgs> bind)
        {
            // Remove at the beginning of the next frame
            _beginFrame += delegate { _bindings.RemoveOnMoved(bind); };
        }

        /// <summary>
        /// Checks if a button is down
        /// </summary>
        /// <param name="button">The button to check</param>
        /// <returns>If the button is down (down = up)</returns>
        public bool IsButtonDown(Button button)
        {
            // Throw if "count" is passed
            if (button == Button.ButtonCount)
                throw new Exception("Button.ButtonCount is not a valid button");

            // Return if button is currently down
            return _isbuttonDown[(int)button];
        }
        /// <summary>
        /// Checks if a button is up
        /// </summary>
        /// <param name="button">The button to check</param>
        /// <returns>If the button is up (true = up)</returns>
        public bool IsButtonUp(Button button)
        {
            // Throw if "count" is passed
            if (button == Button.ButtonCount)
                throw new Exception("Button.ButtonCount is not a valid button");

            // Return if button is currently down
            return !_isbuttonDown[(int)button];
        }

        /// <summary>
        /// Checks if any button is currently down
        /// </summary>
        /// <returns>If any button is down (true = down)</returns>
        public bool IsAnyButtonDown()
        {
            // Return if button is currently down
            return _isbuttonDown[(int)Button.ButtonCount];
        }

        /// <summary>
        /// Gets the input state of a button
        /// </summary>
        /// <param name="button">The button</param>
        /// <returns>The state of the button (Up, Pressed, Down, Released)</returns>
        public InputState GetButtonState(Button button)
        {
            // Throw if "count" is passed
            if (button == Button.ButtonCount)
                throw new Exception("Button.ButtonCount is not a valid button");

            // 
            int index = (int)button;
            if (_isbuttonDown[index])
            {
                if (_oldButtonDown[index])
                    return InputState.Down;
                return InputState.Pressed;
            }

            if (_oldButtonDown[index])
                return InputState.Released;
            return InputState.Up;
        }

        /// <summary>
        /// Set the current position of the mouse
        /// This function sets the current position of the mouse
        /// cursor in desktop coordinates.
        /// </summary>
        /// <param name="position">New position of the mouse</param>
        public void SetDesktopPosition(Vector2i position)
        {
            sfMouse_setPosition(position, IntPtr.Zero);
        }
        /// <summary>
        /// Set the current position of the mouse
        /// This function sets the current position of the mouse
        /// cursor relative to the window.
        /// </summary>
        /// <param name="position">New position of the mouse (realtive to the top-left corner of the window)</param>
        public void SetPosition(Vector2i position)
        {
            _window.SetMousePosition(position);
        }

        /// <summary>
        /// Get the current position of the mouse
        /// This function returns the current position of the mouse
        /// cursor in desktop coordinates.
        /// </summary>
        /// <returns>Current position of the mouse</returns>
        public Vector2i GetDesktopPosition()
        {
            return sfMouse_getPosition(IntPtr.Zero);
        }
        /// <summary>
        /// Get the current position of the mouse
        /// This function returns the current position of the mouse
        /// cursor relative to the window.
        /// </summary>
        /// <param name="relativeTo">Reference window</param>
        /// <returns>Current position of the mouse (realtive to the top-left corner of the window)</returns>
        public Vector2i GetPosition()
        {
            return _window.GetMousePosition();
        }

        // Window
        internal void ApplyWindow(GameWindow window)
        {
            // Apply events to window
            window.MouseButtonPressed += OnButtonPressed;
            window.MouseButtonReleased += OnButtonReleased;
            window.MouseWheelMoved += OnWheelMoved;
            window.MouseMoved += OnMoved;

            // Keep window
            _window = window;
        }
        internal void RemoveWindow(GameWindow window)
        {
            // Remove events from window
            window.MouseButtonPressed -= OnButtonPressed;
            window.MouseButtonReleased -= OnButtonReleased;
            window.MouseWheelMoved -= OnWheelMoved;
            window.MouseMoved -= OnMoved;

            // Forget window
            _window = null;
        }

        // Events
        private void OnButtonPressed(object sender, SFMLMouseButtonEventArgs e)
        {
            // Update button register
            _isbuttonDown[(int)e.Button] = true;

            // Convert event args
            MouseButtonEventArgs args = new MouseButtonEventArgs(e);

            // Call events next update
            _update += delegate
                {
                    // Call game states events
                    foreach (MouseBindingCollection c in _gameBindings.Values)
                        c.OnPressed(args);

                    // Call direct bindings
                    _bindings.OnPressed(args);
                };

            GameConsole.WriteLine(string.Format("InputMouse: Pressed {0}@{1};{2}", e.Button, e.X, e.Y)); // Debug
        }
        private void OnButtonReleased(object sender, SFMLMouseButtonEventArgs e)
        {
            // Update button register
            _isbuttonDown[(int)e.Button] = false;

            // Convert event args
            MouseButtonEventArgs args = new MouseButtonEventArgs(e);

            // Call events next update
            _update += delegate
            {
                // Call game states events
                foreach (MouseBindingCollection c in _gameBindings.Values)
                    c.OnReleased(args);

                // Call direct bindings
                _bindings.OnReleased(args);
            };

            GameConsole.WriteLine(string.Format("InputMouse: Released {0}@{1};{2}", e.Button, e.X, e.Y)); // Debug
        }
        private void OnWheelMoved(object sender, SFMLMouseWheelEventArgs e)
        {
            // Convert event args
            MouseWheelEventArgs args = new MouseWheelEventArgs(e);

            // Call events next update
            _update += delegate
            {
                // Call game states events
                foreach (MouseBindingCollection c in _gameBindings.Values)
                    c.OnWheelMoved(args);

                // Call direct bindings
                _bindings.OnWheelMoved(args);
            };

            GameConsole.WriteLine(string.Format("InputMouse: Scrolled wheel {0}@{1};{2}", e.Delta, e.X, e.Y)); // Debug
        }
        private void OnMoved(object sender, SFMLMouseMoveEventArgs e)
        {
            // Convert event args
            MouseMoveEventArgs args = new MouseMoveEventArgs(e);

            // Call events next update
            _update += delegate
            {
                // Call game states events
                foreach (MouseBindingCollection c in _gameBindings.Values)
                    c.OnMoved(args);

                // Call direct bindings
                _bindings.OnMoved(args);
            };

            // Push mouse position next update
            _update += delegate
                {
                    // Keep current position as previous
                    _prevX = _x;
                    _prevY = _y;

                    // Keep new position as current
                    _x = e.X;
                    _y = e.Y;
                };

            //GameConsole.WriteLine(string.Format("InputMouse: Moved mouse to {0};{1}", e.X, e.Y)); // Debug
        }

        //
        private void PushButtonRegister()
        {
            int length = (int)Button.ButtonCount;
            for (int i = 0; i < length; i++)
                _oldButtonDown[i] = _isbuttonDown[i];
        }
        private void PushMousePosition()
        {
            _oldX = _x;
            _oldY = _y;
        }

        #region SFML Imports
        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern bool sfMouse_isButtonPressed(Button button);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2i sfMouse_getPosition(IntPtr relativeTo);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfMouse_setPosition(Vector2i position, IntPtr relativeTo);
        #endregion
    }
}
