using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;
using BubbasEngine.Engine.Input.Devices;
using BubbasEngine.Engine.Windows;
using BubbasEngine.Engine.GameStates;

namespace BubbasEngine.Engine.Input.Devices
{
    // Event args
    public class KeyboardKeyEventArgs : EventArgs
    {
        public Keyboard.Key Key;

        public bool Alt;
        public bool Control;
        public bool Shift;
        public bool System;

        public KeyboardKeyEventArgs()
        {
        }
        public KeyboardKeyEventArgs(Keyboard.Key key, bool alt, bool control, bool shift, bool system)
        {
            Key = key;
            Alt = alt;
            Control = control;
            Shift = shift;
            System = system;
        }
    }

    public class KeyboardDevice
    {
        // Private
        private bool[] _isKeyDown;

        private KeyboardBindingCollection _bindings; // Directly bound input (through the input class)
        private Dictionary<GameState, KeyboardBindingCollection> _gameBindings; // Input bound through game states

        private EventHandler<KeyEventArgs> _onKeyPressedEvent;
        private EventHandler<KeyEventArgs> _onKeyReleasedEvent;

        private Action _beginFrame;
        private Action _update;

        // Constructor(s)
        internal KeyboardDevice()
        {
            _isKeyDown = new bool[(int)Keyboard.Key.KeyCount];

            _onKeyPressed = new Dictionary<Keyboard.Key, List<KeyboardBinding>>();
            _onKeyReleased = new Dictionary<Keyboard.Key, List<KeyboardBinding>>();
            
            _onKeyPressedEvent = new EventHandler<KeyEventArgs>(OnKeyPressed);
            _onKeyReleasedEvent = new EventHandler<KeyEventArgs>(OnKeyReleased);
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

        // Update
        internal void Update(bool focus)
        {
            // 
            if (focus)
            {
                // Apply device updates
                if (_update != null)
                    _update();
            }

            //
            _update = null;
        }

        //
        internal KeyboardBindingCollection CreateGameBindigs(GameState gs)
        {
            // Create new bindings collection
            KeyboardBindingCollection collection = new KeyboardBindingCollection();
            _gameBindings[gs] = collection;

            // Return collection
            return collection;
        }

        // Add
        public void AddOnPressed(Keyboard.Key key, KeyboardBinding bind)
        {
            _beginFrame += delegate
            {
                // Create if it doesnt exist
                if (!_onKeyPressed.ContainsKey(key))
                    _onKeyPressed.Add(key, new List<KeyboardBinding>());

                // Add
                _onKeyPressed[key].Add(bind);
            };
        }
        public void AddOnReleased(Keyboard.Key key, KeyboardBinding bind)
        {
            _beginFrame += delegate
            {
                // Create if it doesnt exist
                if (!_onKeyReleased.ContainsKey(key))
                    _onKeyReleased.Add(key, new List<KeyboardBinding>());

                // Add
                _onKeyReleased[key].Add(bind);
            };
        }

        // Remove
        public void RemoveOnPressed(Keyboard.Key key, KeyboardBinding bind)
        {
            _beginFrame += delegate
            {
                // Remove
                if (_onKeyPressed.ContainsKey(key))
                {
                    // Remove keybinding
                    if (!_onKeyPressed[key].Remove(bind))
                    {
                        GameConsole.WriteLine(string.Format("InputKeyboard: Tried to remove non-exsiting keybinding (Key:{0}, OnPressed)", key), GameConsole.MessageType.Error); // Debug
                        return;
                    }

                    // Remove this key from the dictionary if there are no keybindings left
                    if (_onKeyPressed[key].Count == 0)
                    {
                        _onKeyPressed.Remove(key);
                        GameConsole.WriteLine(string.Format("InputKeyboard: No more keybindings to this key - therefore remove key (Key:{0}, OnPressed)", key), GameConsole.MessageType.Important); // Debug
                    }
                }
                else
                    GameConsole.WriteLine(string.Format("InputKeyboard: Tried to remove keybinding from non-bound key (Key:{0}, OnPressed)", key), GameConsole.MessageType.Error); // Debug
            };
        }
        public void RemoveOnReleased(Keyboard.Key key, KeyboardBinding bind)
        {
            _beginFrame += delegate
            {
                // Remove
                if (_onKeyReleased.ContainsKey(key))
                {
                    // Remove keybinding
                    if (!_onKeyReleased[key].Remove(bind))
                    {
                        GameConsole.WriteLine(string.Format("InputKeyboard: Tried to remove non-exsiting keybinding (Key:{0}, OnReleased)", key), GameConsole.MessageType.Error); // Debug
                        return;
                    }

                    // Remove this key from the dictionary if there are no keybindings left
                    if (_onKeyPressed[key].Count == 0)
                    {
                        _onKeyReleased.Remove(key);
                        GameConsole.WriteLine(string.Format("InputKeyboard: No more keybindings to this key - therefore remove key (Key:{0}, OnReleased)", key), GameConsole.MessageType.Important); // Debug
                    }
                }
                else
                    GameConsole.WriteLine(string.Format("InputKeyboard: Tried to remove keybinding from non-bound key (Key:{0}, OnReleased)", key), GameConsole.MessageType.Error); // Debug
            };
        }

        // Window
        internal void ApplyWindow(GameWindow window)
        {
            // Apply events to windows
            window.KeyPressed += _onKeyPressedEvent;
            window.KeyReleased += _onKeyReleasedEvent;
        }
        internal void RemoveWindow(GameWindow window)
        {
            // Remove events from windows
            window.KeyPressed -= _onKeyPressedEvent;
            window.KeyReleased -= _onKeyReleasedEvent;
        }

        // Events
        private void OnKeyPressed(object sender, KeyEventArgs e)
        {
            //
            if (e.Code == Keyboard.Key.Unknown)
                return;

            // Update key register
            _isKeyDown[(int)e.Code] = true;

            // Check if the key is bound to anything
            if (_onKeyPressed.ContainsKey(e.Code))
            {
                // Current list (One per key)
                List<KeyboardBinding> list = _onKeyPressed[e.Code];

                int listLength = list.Count;
                for (int i = 0; i < listLength; i++)
                {
                    // Current Keybinding
                    KeyboardBinding bind = list[i];

                    if (bind != null)
                    {
                        _update += delegate
                        {
                            //
                            Keyboard.Key[] mod = bind.GetModifiers();
                            if (mod != null)
                            {
                                // Check modifiers
                                int length = mod.Length;
                                for (int j = 0; j < length; j++)
                                    if (!_isKeyDown[(int)mod[j]]) // If the modifier is not pressed
                                        return; // Don't call the method
                            }

                            // Call the method
                            bind.CallMethod();
                        };
                    }
                }
            }

            GameConsole.WriteLine(string.Format("InputKeyboard: Pressed {0}", e.Code)); // Debug
        }
        private void OnKeyReleased(object sender, KeyEventArgs e)
        {
            //
            if (e.Code == Keyboard.Key.Unknown)
                return;

            // Update key register
            _isKeyDown[(int)e.Code] = false;

            // Check if the key is bound to anything
            if (_onKeyReleased.ContainsKey(e.Code))
            {
                // Current list (One per key)
                List<KeyboardBinding> list = _onKeyReleased[e.Code];

                int listLength = list.Count;
                for (int i = 0; i < listLength; i++)
                {
                    // Current Keybinding
                    KeyboardBinding bind = list[i];

                    if (bind != null)
                    {
                        _update += delegate
                        {
                            //
                            Keyboard.Key[] mod = bind.GetModifiers();
                            if (mod != null)
                            {
                                // Check modifiers
                                int length = mod.Length;
                                for (int j = 0; j < length; j++)
                                    if (!_isKeyDown[(int)mod[j]]) // If the modifier is not pressed
                                        return; // Don't call the method
                            }

                            // Call the method
                            bind.CallMethod();
                        };
                    }
                }
            }

            GameConsole.WriteLine(string.Format("InputKeyboard: Released {0}", e.Code)); // Debug
        }
    }
}
