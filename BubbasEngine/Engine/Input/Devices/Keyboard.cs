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
    public class KeyEventArgs
    {
        public Keyboard.Key Key;

        public bool Alt;
        public bool Control;
        public bool Shift;
        public bool System;
            
        public KeyEventArgs()
        {
        }
        internal KeyEventArgs(SFMLKeyEventArgs args)
        {
            Key = (Keyboard.Key)(int)args.Code;
            Alt = args.Alt;
            Control = args.Control;
            Shift = args.Shift;
            System = args.System;
        }
        public KeyEventArgs(Keyboard.Key key, bool alt, bool control, bool shift, bool system)
        {
            Key = key;
            Alt = alt;
            Control = control;
            Shift = shift;
            System = system;
        }
    }

    public class Keyboard
    {
        /// <summary>
        /// Key codes
        /// </summary>
        public enum Key
        {
            Unknown = -1, // Unhandled key
            A = 0,        // The A key
            B,            // The B key
            C,            // The C key
            D,            // The D key
            E,            // The E key
            F,            // The F key
            G,            // The G key
            H,            // The H key
            I,            // The I key
            J,            // The J key
            K,            // The K key
            L,            // The L key
            M,            // The M key
            N,            // The N key
            O,            // The O key
            P,            // The P key
            Q,            // The Q key
            R,            // The R key
            S,            // The S key
            T,            // The T key
            U,            // The U key
            V,            // The V key
            W,            // The W key
            X,            // The X key
            Y,            // The Y key
            Z,            // The Z key
            Num0,         // The 0 key
            Num1,         // The 1 key
            Num2,         // The 2 key
            Num3,         // The 3 key
            Num4,         // The 4 key
            Num5,         // The 5 key
            Num6,         // The 6 key
            Num7,         // The 7 key
            Num8,         // The 8 key
            Num9,         // The 9 key
            Escape,       // The Escape key
            LControl,     // The left Control key
            LShift,       // The left Shift key
            LAlt,         // The left Alt key
            LSystem,      // The left OS specific key: window (Windows and Linux), apple (MacOS X), ...
            RControl,     // The right Control key
            RShift,       // The right Shift key
            RAlt,         // The right Alt key
            RSystem,      // The right OS specific key: window (Windows and Linux), apple (MacOS X), ...
            Menu,         // The Menu key
            LBracket,     // The [ key
            RBracket,     // The ] key
            SemiColon,    // The ; key
            Comma,        // The , key
            Period,       // The . key
            Quote,        // The ' key
            Slash,        // The / key
            BackSlash,    // The \ key
            Tilde,        // The ~ key
            Equal,        // The = key
            Dash,         // The - key
            Space,        // The Space key
            Return,       // The Return key
            Back,         // The Backspace key
            Tab,          // The Tabulation key
            PageUp,       // The Page up key
            PageDown,     // The Page down key
            End,          // The End key
            Home,         // The Home key
            Insert,       // The Insert key
            Delete,       // The Delete key
            Add,          // +
            Subtract,     // -
            Multiply,     // *
            Divide,       // /
            Left,         // Left arrow
            Right,        // Right arrow
            Up,           // Up arrow
            Down,         // Down arrow
            Numpad0,      // The numpad 0 key
            Numpad1,      // The numpad 1 key
            Numpad2,      // The numpad 2 key
            Numpad3,      // The numpad 3 key
            Numpad4,      // The numpad 4 key
            Numpad5,      // The numpad 5 key
            Numpad6,      // The numpad 6 key
            Numpad7,      // The numpad 7 key
            Numpad8,      // The numpad 8 key
            Numpad9,      // The numpad 9 key
            F1,           // The F1 key
            F2,           // The F2 key
            F3,           // The F3 key
            F4,           // The F4 key
            F5,           // The F5 key
            F6,           // The F6 key
            F7,           // The F7 key
            F8,           // The F8 key
            F9,           // The F8 key
            F10,          // The F10 key
            F11,          // The F11 key
            F12,          // The F12 key
            F13,          // The F13 key
            F14,          // The F14 key
            F15,          // The F15 key
            Pause,        // The Pause key

            KeyCount      // Keep last -- the total number of keyboard keys
        }

        // Private
        private bool[] _isKeyDown;
        private bool[] _oldKeyDown;

        private KeyboardBindingCollection _bindings; // Directly bound input (through the input class)
        private Dictionary<GameState, KeyboardBindingCollection> _gameBindings; // Input bound through game states

        private Action _beginFrame;
        private Action _update;

        // Constructor(s)
        internal Keyboard()
        {
            // Create key register
            _isKeyDown = new bool[(int)Key.KeyCount + 1];
            _oldKeyDown = new bool[(int)Key.KeyCount + 1];

            // Create keybinding containers
            _bindings = new KeyboardBindingCollection();
            _gameBindings = new Dictionary<GameState, KeyboardBindingCollection>();
        }

        // Gameloop
        internal void BeginFrame()
        {
            if (_beginFrame != null)
            {
                _beginFrame();
                _beginFrame = null;
            }
        }
        internal void Update(bool focus)
        {
            // Push key register
            PushButtonRegister();

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
        internal KeyboardBindingCollection CreateGameBindigs(GameState gs)
        {
            // Create new bindings collection
            KeyboardBindingCollection collection = new KeyboardBindingCollection();
            _gameBindings[gs] = collection;

            // Return collection
            return collection;
        }
        internal void RemoveGameBindings(GameState gs)
        {
            // Remove bindings collection
            _gameBindings.Remove(gs);
        }

        // Add
        public void AddOnPressed(Key key, DeleHandler<KeyEventArgs> bind)
        {
            // Add at the beginning of the next frame
            _beginFrame += delegate { _bindings.AddOnPressed(key, bind); };
        }
        public void AddOnReleased(Key key, DeleHandler<KeyEventArgs> bind)
        {
            // Add at the beginning of the next frame
            _beginFrame += delegate { _bindings.AddOnReleased(key, bind); };
        }

        // Remove
        public void RemoveOnPressed(Key key, DeleHandler<KeyEventArgs> bind)
        {
            // Add at the beginning of the next frame
            _beginFrame += delegate { _bindings.RemoveOnPressed(key, bind); };
        }
        public void RemoveOnReleased(Key key, DeleHandler<KeyEventArgs> bind)
        {
            // Add at the beginning of the next frame
            _beginFrame += delegate { _bindings.RemoveOnReleased(key, bind); };
        }

        // Window
        internal void ApplyWindow(GameWindow window)
        {
            // Apply events to windows
            window.KeyPressed += OnKeyPressed;
            window.KeyReleased += OnKeyReleased;
        }
        internal void RemoveWindow(GameWindow window)
        {
            // Remove events from windows
            window.KeyPressed -= OnKeyPressed;
            window.KeyReleased -= OnKeyReleased;
        }

        // Events
        internal void OnKeyPressed(object sender, SFMLKeyEventArgs e)
        {
            // Update button register
            _isKeyDown[(int)e.Code + 1] = true;

            // Convert event args
            KeyEventArgs args = new KeyEventArgs(e);

            // Call events next update
            _update += delegate
            {
                // Call game states events
                foreach (KeyboardBindingCollection c in _gameBindings.Values)
                    c.OnPressed(args);

                // Call direct bindings
                _bindings.OnPressed(args);
            };

            GameConsole.WriteLine(string.Format("InputKeyboard: Pressed {0}", e.Code)); // Debug
        }
        internal void OnKeyReleased(object sender, SFMLKeyEventArgs e)
        {
            // Update button register
            _isKeyDown[(int)e.Code + 1] = false;

            // Convert event args
            KeyEventArgs args = new KeyEventArgs(e);

            // Call events next update
            _update += delegate
            {
                // Call game states events
                foreach (KeyboardBindingCollection c in _gameBindings.Values)
                    c.OnReleased(args);

                // Call direct bindings
                _bindings.OnReleased(args);
            };

            GameConsole.WriteLine(string.Format("InputKeyboard: Released {0}", e.Code)); // Debug
        }

        //
        private void PushButtonRegister()
        {
            int length = (int)Key.KeyCount + 1;
            for (int i = 0; i < length; i++)
                _oldKeyDown[i] = _isKeyDown[i];
        }

        #region Imports
        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern bool sfKeyboard_isKeyPressed(Key Key);
        #endregion
    }
}
