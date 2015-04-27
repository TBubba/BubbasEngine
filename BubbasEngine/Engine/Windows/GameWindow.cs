using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;
using SFML.Graphics;

namespace BubbasEngine.Engine.Windows
{
    public class TitleChangedEventArgs : EventArgs
    {
        public string Title;

        public TitleChangedEventArgs()
        {
        }
        public TitleChangedEventArgs(string title)
        {
            Title = title;
        }
    }

    public class ClosingWindowEventArgs : EventArgs
    {
        public bool Shut;

        public ClosingWindowEventArgs()
        {
        }
        public ClosingWindowEventArgs(bool shut)
        {
            Shut = shut;
        }
    }

    public class ShutWindowEventArgs : EventArgs
    {
    }

    public class GameWindow
    {
        // Private class
        private class ExposedWindow : RenderWindow
        {
            // Private
            private string _title;
            private Action _onDispatch; // Additional functions to call when dispatching

            // Internal
            internal string Title
            { get { return _title; } }

            // Constructor(s)
            internal ExposedWindow(IntPtr handle)
                : base(handle)
            {
                Initialize();
            }
            internal ExposedWindow(IntPtr handle, ContextSettings settings)
                : base(handle, settings)
            {
                Initialize();
            }
            internal ExposedWindow(VideoMode mode, string title)
                : base(mode, title)
            {
                Initialize();
            }
            internal ExposedWindow(VideoMode mode, string title, Styles style)
                : base(mode, title, style)
            {
                Initialize();
            }
            internal ExposedWindow(VideoMode mode, string title, Styles style, ContextSettings settings)
                : base(mode, title, style, settings)
            {
                Initialize();
            }

            // Initialize
            private void Initialize()
            {
                //
                Closed += new EventHandler(ExposedWindow_Closed);
            }

            private void ExposedWindow_Closed(object sender, EventArgs e)
            {
                ClosingWindowEventArgs ce = new ClosingWindowEventArgs() { Shut = true };
                
                // Dispatch event
                if (Closing != null)
                    Closing(this, ce);

                // Shut down if no methods says otherwise
                if (ce.Shut)
                {
                    if (Shut != null) // (Should ever be null, unless changes in engine source-code)
                        Shut(this, null);
                }
            }

            // Events
            internal event EventHandler<TitleChangedEventArgs> TitleChanged;
            internal event EventHandler<ClosingWindowEventArgs> Closing;
            internal event EventHandler<ShutWindowEventArgs> Shut;
              
            // Set Focus (force)
            internal void SetFocus()
            {
                _onDispatch += delegate
                    {
                    NativeMethods.SwitchToThisWindow(SystemHandle, true);
                    };
            }

            // Set Title
            public override void SetTitle(string title)
            {
                // Set title
                _title = title;
 	            base.SetTitle(title);

                // Dispatch 'TitleChanged' event when dispatching
                _onDispatch += delegate
                    {
                        TitleChanged(this, new TitleChangedEventArgs() { Title = title });
                    };
            }

            // Dispatch
            internal void DispatchExposedEvents()
            {
                if (_onDispatch != null)
                {
                    _onDispatch();
                    _onDispatch = null;
                }
            }
        }

        // Private
        private ExposedWindow _window;
        private bool _focused;
        private bool _open = true;

        // EventHandlers
        EventHandler _gainedFocus;
        EventHandler _lostFocus;
        EventHandler<ShutWindowEventArgs> _shutWindow;

        // Internal
        internal RenderTarget Target
        { get { return _window; } }
        internal bool Open
        { get { return _open; } }

        // Public
        public Vector2i Position
        { get { return _window.Position; } }
        public bool Focused
        { get { return _focused; } }

        public uint Height
        { get { return _window.Size.Y; } }
        public uint Width
        { get { return _window.Size.X; } }
        public Vector2u Size
        { get { return _window.Size; } }

        public string Title
        { get { return _window.Title; } set { _window.SetTitle(value); } }

        // Constructor(s)
        internal GameWindow(GameWindowArgs args)
        {
            // Window (Create a new /or/ Render to existing)
            if (args.CreateWindow)
            {
                // Create window
                _window = new ExposedWindow(new VideoMode(args.WindowWidth, args.WindowHeight), 
                                            args.WindowTitle,
                                            args.WindowStyle);
                _window.SetFocus();

                // Focus on click
                MouseButtonPressed += new EventHandler<MouseButtonEventArgs>(delegate
                    {
                        if (!_focused)
                            _window.SetFocus();
                    });
            }
            else
            {
                // Render to existing
                _window = new ExposedWindow(args.WindowPointer);
            }

            // Window is focused (so set focus to true)
            _focused = true;

            // Set window settings
            _window.SetVerticalSyncEnabled(false);
            _window.SetFramerateLimit(0u);
            _window.SetActive(true);

            // Set up EventHandlers
            _gainedFocus += new EventHandler(delegate { _focused = true; });
            _lostFocus += new EventHandler(delegate { _focused = false; });
            _shutWindow += new EventHandler<ShutWindowEventArgs>(delegate { _open = false; });

            // Apply EventHandlers to window
            _window.GainedFocus += _gainedFocus;
            _window.LostFocus += _lostFocus;
            _window.Shut += _shutWindow;
        }

        // Events
        public event EventHandler GainedFocus
        { add { _window.GainedFocus += value; } remove { _window.GainedFocus -= value; } }
        internal event EventHandler<JoystickButtonEventArgs> JoystickButtonPressed
        { add { _window.JoystickButtonPressed += value; } remove { _window.JoystickButtonPressed -= value; } }
        internal event EventHandler<JoystickButtonEventArgs> JoystickButtonReleased
        { add { _window.JoystickButtonReleased += value; } remove { _window.JoystickButtonReleased -= value; } }
        internal event EventHandler<JoystickConnectEventArgs> JoystickConnected
        { add { _window.JoystickConnected += value; } remove { _window.JoystickConnected -= value; } }
        internal event EventHandler<JoystickConnectEventArgs> JoystickDisconnected
        { add { _window.JoystickDisconnected += value; } remove { _window.JoystickDisconnected -= value; } }
        internal event EventHandler<JoystickMoveEventArgs> JoystickMoved
        { add { _window.JoystickMoved += value; } remove { _window.JoystickMoved -= value; } }
        internal event EventHandler<KeyEventArgs> KeyPressed
        { add { _window.KeyPressed += value; } remove { _window.KeyPressed -= value; } }
        internal event EventHandler<KeyEventArgs> KeyReleased
        { add { _window.KeyReleased += value; } remove { _window.KeyReleased -= value; } }
        public event EventHandler LostFocus
        { add { _window.LostFocus += value; } remove { _window.LostFocus -= value; } }
        internal event EventHandler<MouseButtonEventArgs> MouseButtonPressed
        { add { _window.MouseButtonPressed += value; } remove { _window.MouseButtonPressed -= value; } }
        internal event EventHandler<MouseButtonEventArgs> MouseButtonReleased
        { add { _window.MouseButtonReleased += value; } remove { _window.MouseButtonReleased -= value; } }
        internal event EventHandler MouseEntered
        { add { _window.MouseEntered += value; } remove { _window.MouseEntered -= value; } }
        internal event EventHandler MouseLeft
        { add { _window.MouseLeft += value; } remove { _window.MouseLeft -= value; } }
        internal event EventHandler<MouseMoveEventArgs> MouseMoved
        { add { _window.MouseMoved += value; } remove { _window.MouseMoved -= value; } }
        internal event EventHandler<MouseWheelEventArgs> MouseWheelMoved
        { add { _window.MouseWheelMoved += value; } remove { _window.MouseWheelMoved -= value; } }
        public event EventHandler<SizeEventArgs> Resized
        { add { _window.Resized += value; } remove { _window.Resized -= value; } }
        internal event EventHandler<TextEventArgs> TextEntered
        { add { _window.TextEntered += value; } remove { _window.TextEntered -= value; } }

        public event EventHandler<ClosingWindowEventArgs> Closing
        { add { _window.Closing += value; } remove { _window.Closing -= value; } }
        public event EventHandler<ShutWindowEventArgs> Shut
        { add { _window.Shut += value; } remove { _window.Shut -= value; } }
        public event EventHandler<TitleChangedEventArgs> TitleChanged
        { add { _window.TitleChanged += value; } remove { _window.TitleChanged -= value; } }

        // Input
        internal void DispatchEvents()
        {
            _window.DispatchEvents();
            _window.DispatchExposedEvents();
        }

        // Drawing
        internal void Clear()
        {
            _window.Clear();
        }
        internal void Display()
        {
            _window.Display();
        }
        internal void Draw(Drawable draw)
        {
            _window.Draw(draw);
        }

        // Close
        internal void Close()
        {
            _window.Close();
        }
    }
}
