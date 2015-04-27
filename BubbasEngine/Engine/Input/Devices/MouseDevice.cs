using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;
using BubbasEngine.Engine.Windows;
using BubbasEngine.Engine.GameStates;

namespace BubbasEngine.Engine.Input.Devices
{
    // Event args
    public class MouseWheelEventArgs2 : EventArgs
    {
        public int X;
        public int Y;
        public int Delta;

        public MouseWheelEventArgs2()
        {
        }
        public MouseWheelEventArgs2(int x, int y, int delta)
        {
            X = x;
            Y = y;
            Delta = delta;
        }
    }
    public class MouseMoveEventArgs2 : EventArgs
    {
        public int X;
        public int Y;

        public MouseMoveEventArgs2()
        {
        }
        public MouseMoveEventArgs2(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    public class MouseButtonEventArgs2 : EventArgs
    {
        public int X;
        public int Y;
        public Mouse.Button Button;

        public MouseButtonEventArgs2()
        {
        }
        public MouseButtonEventArgs2(int x, int y, Mouse.Button button)
        {
            X = x;
            Y = y;
            Button = button;
        }
    }


    public class MouseDevice
    {
        // Private
        private bool[] _buttonDown; // Button states (true = down)

        private int _x; // Mouse position
        private int _y;

        private int _oldX; // Old mouse position
        private int _oldY;

        private MouseBindingCollection _bindings; // Directly bound input (through the input class)
        private Dictionary<GameState, MouseBindingCollection> _gameBindings; // Input bound through game states

        private EventHandler<MouseButtonEventArgs> _onButtonPressedEvent; // Handlers for window events
        private EventHandler<MouseButtonEventArgs> _onButtonReleasedEvent;
        private EventHandler<MouseWheelEventArgs> _onWheelMovedEvent;
        private EventHandler<MouseMoveEventArgs> _onMovedEvent;

        private Action _beginFrame;
        private Action _update;

        private GameWindow _window;

        // Public
        public int X
        { get { return _x; } }
        public int Y
        { get { return _y; } }

        public int OldX
        { get { return _oldX; } }
        public int OldY
        { get { return _oldY; } }

        // Constructor(s)
        internal MouseDevice()
        {
            //
            _buttonDown = new bool[(int)Mouse.Button.ButtonCount];

            //
            _bindings = new MouseBindingCollection();
            _gameBindings = new Dictionary<GameState, MouseBindingCollection>();

            // Create Events
            _onButtonPressedEvent = new EventHandler<MouseButtonEventArgs>(OnButtonPressed);
            _onButtonReleasedEvent = new EventHandler<MouseButtonEventArgs>(OnButtonReleased);
            _onWheelMovedEvent = new EventHandler<MouseWheelEventArgs>(OnWheelMoved);
            _onMovedEvent = new EventHandler<MouseMoveEventArgs>(OnMoved);
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
        internal void Update(bool focus)
        {
            //
            if (focus)
            {
                //
                if (_update != null)
                {
                    _update();
                    _update = null;
                }
            }
        }

        //
        internal MouseBindingCollection CreateGameBindigs(GameState gs)
        {
            // Create new bindings collection
            MouseBindingCollection collection = new MouseBindingCollection();
            _gameBindings[gs] = collection;

            // Return collection
            return collection;
        }

        // Add
        public void AddOnPressed(Mouse.Button button, EventHandler<MouseButtonEventArgs2> bind)
        {
            _beginFrame += delegate
            {
                // Create if it doesnt exist
                if (!_onButtonPressed.ContainsKey(button))
                    _onButtonPressed.Add(button, new List<MouseButtonBinding>());

                // Add
                _onButtonPressed[button].Add(bind);
            };
        }
        public void AddOnReleased(Mouse.Button button, EventHandler<MouseButtonEventArgs2> bind)
        {
            _beginFrame += delegate
            {
                // Create if it doesnt exist
                if (!_onButtonReleased.ContainsKey(button))
                    _onButtonReleased.Add(button, new List<MouseButtonBinding>());

                // Add
                _onButtonReleased[button].Add(bind);
            };
        }
        public void AddOnWheelChanged(EventHandler<MouseWheelEventArgs2> bind)
        {
            _beginFrame += delegate
            {
                // Add
                _onWheelMoved.Add(bind);
            };
        }
        public void AddOnMoved(EventHandler<MouseMoveEventArgs2> bind)
        {
            _beginFrame += delegate
            {
                // Add
                _onMoved.Add(bind);
            };
        }

        // Remove
        public void RemoveOnPressed(Mouse.Button button, EventHandler<MouseButtonEventArgs2> bind)
        {
            _beginFrame += delegate
            {
                // Remove
                if (_onButtonPressed.ContainsKey(button))
                {
                    // Remove keybinding
                    if (!_onButtonPressed[button].Remove(bind))
                    {
                        GameConsole.WriteLine(string.Format("InputMouse: Tried to remove non-exsiting keybinding (Button:{0}, OnPressed)", button), GameConsole.MessageType.Error); // Debug
                        return;
                    }

                    // Remove this key from the dictionary if there are no keybindings left
                    if (_onButtonPressed[button].Count == 0)
                    {
                        _onButtonPressed.Remove(button);
                        GameConsole.WriteLine(string.Format("InputMouse: No more keybindings to this button - therefore remove button (Button:{0}, OnPressed)", button), GameConsole.MessageType.Important); // Debug
                    }
                }
                else
                    GameConsole.WriteLine(string.Format("InputMouse: Tried to remove keybinding from non-bound button (Button:{0}, OnPressed)", button), GameConsole.MessageType.Error); // Debug
            };
        }
        public void RemoveOnReleased(Mouse.Button button, EventHandler<MouseButtonEventArgs2> bind)
        {
            _beginFrame += delegate
            {
                // Remove
                if (_onButtonReleased.ContainsKey(button))
                {
                    // Remove keybinding
                    if (!_onButtonReleased[button].Remove(bind))
                    {
                        GameConsole.WriteLine(string.Format("InputMouse: Tried to remove non-exsiting keybinding (Button:{0}, OnReleased)", button), GameConsole.MessageType.Error); // Debug
                        return;
                    }

                    // Remove this key from the dictionary if there are no keybindings left
                    if (_onButtonReleased[button].Count == 0)
                    {
                        _onButtonReleased.Remove(button);
                        GameConsole.WriteLine(string.Format("InputMouse: No more keybindings to this button - therefore remove button (Button:{0}, OnReleased)", button), GameConsole.MessageType.Important); // Debug
                    }
                }
                else
                    GameConsole.WriteLine(string.Format("InputMouse: Tried to remove keybinding from non-bound button (Button:{0}, OnReleased)", button), GameConsole.MessageType.Error); // Debug
            };
        }
        public void RemoveOnWheelChanged(EventHandler<MouseWheelEventArgs2> bind)
        {
            _beginFrame += delegate
            {
                // Remove
                if (_onWheelMoved.Contains(bind))
                {
                    _onWheelMoved.Remove(bind);
                }
                else
                    GameConsole.WriteLine(string.Format("InputMouse: Tried to remove non-exsiting keybinding (OnWheelChanged)"), GameConsole.MessageType.Error); // Debug
            };
        }
        public void RemoveOnMoved(EventHandler<MouseMoveEventArgs2> bind)
        {
            _beginFrame += delegate
            {
                // Remove
                if (_onMoved.Contains(bind))
                {
                    _onMoved.Remove(bind);
                }
                else
                    GameConsole.WriteLine(string.Format("InputMouse: Tried to remove non-exsiting keybinding (OnMoved)"), GameConsole.MessageType.Error); // Debug
            };
        }


        // Window
        internal void ApplyWindow(GameWindow window)
        {
            // Apply events to windows
            window.MouseButtonPressed += _onButtonPressedEvent;
            window.MouseButtonReleased += _onButtonReleasedEvent;
            window.MouseWheelMoved += _onWheelMovedEvent;
            window.MouseMoved += _onMovedEvent;

            // Keep window
            _window = window;
        }
        internal void RemoveWindow(GameWindow window)
        {
            // Remove events from windows
            window.MouseButtonPressed -= _onButtonPressedEvent;
            window.MouseButtonReleased -= _onButtonReleasedEvent;
            window.MouseWheelMoved -= _onWheelMovedEvent;
            window.MouseMoved -= _onMovedEvent;

            // Forget window
            _window = window;
        }

        //


        // Events
        private void OnButtonPressed(object sender, MouseButtonEventArgs e)
        {
            // Update key register
            _buttonDown[(int)e.Button] = true;

            // Check if the key is bound to anything
            if (_onButtonPressed.ContainsKey(e.Button))
            {
                // Current list (One per key)
                List<MouseButtonBinding> list = _onButtonPressed[e.Button];

                int listLength = list.Count;
                for (int i = 0; i < listLength; i++)
                {
                    // Current Keybinding
                    MouseButtonBinding bind = list[i];

                    if (bind != null)
                    {
                        _update += delegate
                        {
                            // Call the method
                            bind.CallMethod(e.X, e.Y);
                        };
                    }
                }
            }

            GameConsole.WriteLine(string.Format("InputMouse: Pressed {0}@{1};{2}", e.Button, e.X, e.Y)); // Debug
        }
        private void OnButtonReleased(object sender, MouseButtonEventArgs e)
        {
            // Update key register
            _buttonDown[(int)e.Button] = false;

            // Check if the key is bound to anything
            if (_onButtonReleased.ContainsKey(e.Button))
            {
                // Current list (One per key)
                List<MouseButtonBinding> list = _onButtonReleased[e.Button];

                int listLength = list.Count;
                for (int i = 0; i < listLength; i++)
                {
                    // Current Keybinding
                    MouseButtonBinding bind = list[i];

                    if (bind != null)
                    {
                        _update += delegate
                        {
                            // Call the method
                            bind.CallMethod(e.X, e.Y);
                        };
                    }
                }
            }

            GameConsole.WriteLine(string.Format("InputMouse: Released {0}@{1};{2}", e.Button, e.X, e.Y)); // Debug
        }
        private void OnWheelMoved(object sender, MouseWheelEventArgs e)
        {
            // Check if the key is bound to anything
            int listLength = _onWheelMoved.Count;
            for (int i = 0; i < listLength; i++)
            {
                // Current Keybinding
                MouseWheelBinding bind = _onWheelMoved[i];

                if (bind != null)
                {
                    _update += delegate
                    {
                        // Call the method
                        bind.CallMethod(e.Delta, e.X, e.Y);
                    };
                }
            }

            GameConsole.WriteLine(string.Format("InputMouse: Scrolled wheel {0}@{1};{2}", e.Delta, e.X, e.Y)); // Debug
        }
        private void OnMoved(object sender, MouseMoveEventArgs e)
        {
            // Check if the key is bound to anything
            int listLength = _onMoved.Count;
            for (int i = 0; i < listLength; i++)
            {
                // Current Keybinding
                MouseMoveBinding bind = _onMoved[i];

                if (bind != null)
                    _update += delegate { bind.CallMethod(e.X, e.Y); };
            }

            _update += delegate
                {
                    // Keep current position as old
                    _oldX = _x;
                    _oldY = _y;

                    // Keep new position as current
                    _x = e.X;
                    _y = e.Y;
                };

            //GameConsole.WriteLine(string.Format("InputMouse: Moved mouse to {0};{1}", e.X, e.Y)); // Debug
        }
    }
}
