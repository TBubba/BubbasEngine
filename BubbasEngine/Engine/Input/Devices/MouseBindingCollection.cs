using System;
using System.Collections.Generic;
using BubbasEngine.Engine.Generic;

namespace BubbasEngine.Engine.Input.Devices
{
    public class MouseBindingCollection
    {
        /// <summary>
        /// Chains a MouseBindingCollection to another
        /// (calling events from "chain" will also call events from "link")
        /// (same as calling chain.Chain(link))
        /// </summary>
        /// <param name="chain">The collection that the "link" will be chained to</param>
        /// <param name="link">The collection that will be chained to the "chain"</param>
        /// <returns>"Chain" parameter</returns>
        public static MouseBindingCollection operator +(MouseBindingCollection chain, MouseBindingCollection link)
        {
            // Add link to chain
            chain.Chain(link);

            // Return chain
            return chain;
        }
        /// <summary>
        /// Dechains a MouseBindingCollection from another
        /// (calling events from "chain" will no longer call events from "link" - if they were chained before)
        /// (same as calling chain.Dechain(link))
        /// </summary>
        /// <param name="chain">The collection that the "link" will be dechained from</param>
        /// <param name="link">The collection that will be dechained to the "chain"</param>
        /// <returns>"Chain" parameter</returns>
        public static MouseBindingCollection operator -(MouseBindingCollection chain, MouseBindingCollection link)
        {
            // Add link to chain
            chain.Dechain(link);

            // Return chain
            return chain;
        }

        // Private
        private Dictionary<Mouse.Button, List<DeleHandler<MouseButtonEventArgs>>> _onButtonPressed;
        private Dictionary<Mouse.Button, List<DeleHandler<MouseButtonEventArgs>>> _onButtonReleased;
        private List<DeleHandler<MouseWheelEventArgs>> _onWheelMoved;
        private List<DeleHandler<MouseMoveEventArgs>> _onMoved;

        private event DeleHandler<MouseButtonEventArgs> _buttonPressed;
        private event DeleHandler<MouseButtonEventArgs> _buttonReleased;
        private event DeleHandler<MouseWheelEventArgs> _wheelMoved;
        private event DeleHandler<MouseMoveEventArgs> _moved;

        // Constructor(s)
        public MouseBindingCollection()
        {
            //
            _onButtonPressed = new Dictionary<Mouse.Button, List<DeleHandler<MouseButtonEventArgs>>>();
            _onButtonReleased = new Dictionary<Mouse.Button, List<DeleHandler<MouseButtonEventArgs>>>();
            _onWheelMoved = new List<DeleHandler<MouseWheelEventArgs>>();
            _onMoved = new List<DeleHandler<MouseMoveEventArgs>>();

            //
            _buttonPressed = OnPressed;
            _buttonReleased = OnReleased;
            _wheelMoved = OnWheelMoved;
            _moved = OnMoved;
        }

        // Add
        public void AddOnPressed(Mouse.Button button, DeleHandler<MouseButtonEventArgs> bind)
        {
            // Throw if "count" is passed
            if (button == Mouse.Button.ButtonCount)
                throw new Exception("Button.ButtonCount is not a valid button");

            // Create if it doesnt exist
            if (!_onButtonPressed.ContainsKey(button))
                _onButtonPressed.Add(button, new List<DeleHandler<MouseButtonEventArgs>>());

            // Add
            _onButtonPressed[button].Add(bind);
        }
        public void AddOnReleased(Mouse.Button button, DeleHandler<MouseButtonEventArgs> bind)
        {
            // Throw if "count" is passed
            if (button == Mouse.Button.ButtonCount)
                throw new Exception("Button.ButtonCount is not a valid button");

            // Create if it doesnt exist
            if (!_onButtonReleased.ContainsKey(button))
                _onButtonReleased.Add(button, new List<DeleHandler<MouseButtonEventArgs>>());

            // Add
            _onButtonReleased[button].Add(bind);
        }
        public void AddOnAnyPressed(DeleHandler<MouseButtonEventArgs> bind)
        {
            // Create if it doesnt exist
            if (!_onButtonPressed.ContainsKey(Mouse.Button.ButtonCount)) // ButtonCount is used as "Any button"
                _onButtonPressed.Add(Mouse.Button.ButtonCount, new List<DeleHandler<MouseButtonEventArgs>>());

            // Add
            _onButtonPressed[Mouse.Button.ButtonCount].Add(bind);
        }
        public void AddOnAnyReleased(DeleHandler<MouseButtonEventArgs> bind)
        {
            // Create if it doesnt exist
            if (!_onButtonReleased.ContainsKey(Mouse.Button.ButtonCount))
                _onButtonReleased.Add(Mouse.Button.ButtonCount, new List<DeleHandler<MouseButtonEventArgs>>());

            // Add
            _onButtonReleased[Mouse.Button.ButtonCount].Add(bind);
        }
        public void AddOnWheelChanged(DeleHandler<MouseWheelEventArgs> bind)
        {
            // Add
            _onWheelMoved.Add(bind);
        }
        public void AddOnMoved(DeleHandler<MouseMoveEventArgs> bind)
        {
            // Add
            _onMoved.Add(bind);
        }

        // Remove
        public void RemoveOnPressed(Mouse.Button button, DeleHandler<MouseButtonEventArgs> bind)
        {
            // Throw if "count" is passed
            if (button == Mouse.Button.ButtonCount)
                throw new Exception("Button.ButtonCount is not a valid button");

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
        }
        public void RemoveOnAnyPressed(DeleHandler<MouseButtonEventArgs> bind)
        {
            // Remove
            if (_onButtonPressed.ContainsKey(Mouse.Button.ButtonCount))
            {
                // Remove keybinding
                if (!_onButtonPressed[Mouse.Button.ButtonCount].Remove(bind))
                {
                    GameConsole.WriteLine("InputMouse: Tried to remove non-exsiting keybinding (OnAnyPressed)", GameConsole.MessageType.Error); // Debug
                    return;
                }

                // Remove this key from the dictionary if there are no keybindings left
                if (_onButtonPressed[Mouse.Button.ButtonCount].Count == 0)
                {
                    _onButtonPressed.Remove(Mouse.Button.ButtonCount);
                    GameConsole.WriteLine("InputMouse: No more keybindings to this button - therefore remove button (OnAnyPressed)", GameConsole.MessageType.Important); // Debug
                }
            }
            else
                GameConsole.WriteLine("InputMouse: Tried to remove keybinding from non-bound button (OnAnyPressed)", GameConsole.MessageType.Error); // Debug
        }
        public void RemoveOnReleased(Mouse.Button button, DeleHandler<MouseButtonEventArgs> bind)
        {
            // Throw if "count" is passed
            if (button == Mouse.Button.ButtonCount)
                throw new Exception("Button.ButtonCount is not a valid button");

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
        }
        public void RemoveOnAnyReleased(DeleHandler<MouseButtonEventArgs> bind)
        {
            // Remove
            if (_onButtonReleased.ContainsKey(Mouse.Button.ButtonCount))
            {
                // Remove keybinding
                if (!_onButtonReleased[Mouse.Button.ButtonCount].Remove(bind))
                {
                    GameConsole.WriteLine("InputMouse: Tried to remove non-exsiting keybinding (OnAnyReleased)", GameConsole.MessageType.Error); // Debug
                    return;
                }

                // Remove this key from the dictionary if there are no keybindings left
                if (_onButtonReleased[Mouse.Button.ButtonCount].Count == 0)
                {
                    _onButtonReleased.Remove(Mouse.Button.ButtonCount);
                    GameConsole.WriteLine("InputMouse: No more keybindings to this button - therefore remove button (OnAnyReleased)", GameConsole.MessageType.Important); // Debug
                }
            }
            else
                GameConsole.WriteLine("InputMouse: Tried to remove keybinding from non-bound button (OnAnyReleased)", GameConsole.MessageType.Error); // Debug
        }
        public void RemoveOnWheelChanged(DeleHandler<MouseWheelEventArgs> bind)
        {
            // Remove
            if (_onWheelMoved.Contains(bind))
            {
                _onWheelMoved.Remove(bind);
            }
            else
                GameConsole.WriteLine(string.Format("InputMouse: Tried to remove non-exsiting keybinding (OnWheelChanged)"), GameConsole.MessageType.Error); // Debug
        }
        public void RemoveOnMoved(DeleHandler<MouseMoveEventArgs> bind)
        {
            // Remove
            if (_onMoved.Contains(bind))
            {
                _onMoved.Remove(bind);
            }
            else
                GameConsole.WriteLine(string.Format("InputMouse: Tried to remove non-exsiting keybinding (OnMoved)"), GameConsole.MessageType.Error); // Debug
        }

        // Chain
        public void Chain(MouseBindingCollection bindings)
        {
            // Chain bindings
            _buttonPressed += bindings._buttonPressed;
            _buttonReleased += bindings._buttonReleased;
            _wheelMoved += bindings._wheelMoved;
            _moved += bindings._moved;
        }
        public void Dechain(MouseBindingCollection bindings)
        {
            // Dechain bindings
            _buttonPressed -= bindings._buttonPressed;
            _buttonReleased -= bindings._buttonReleased;
            _wheelMoved -= bindings._wheelMoved;
            _moved -= bindings._moved;
        }

        // Call
        internal void OnPressed(MouseButtonEventArgs args)
        {
            // Call button pressed
            if (_onButtonPressed.ContainsKey(args.Button))
                CallEventList<MouseButtonEventArgs>(_onButtonPressed[args.Button], args);

            // Call any button pressed
            if (_onButtonPressed.ContainsKey(Mouse.Button.ButtonCount))
                CallEventList<MouseButtonEventArgs>(_onButtonPressed[Mouse.Button.ButtonCount], args);
        }
        internal void OnReleased(MouseButtonEventArgs args)
        {
            // Call button released
            if (_onButtonReleased.ContainsKey(args.Button))
                CallEventList<MouseButtonEventArgs>(_onButtonReleased[args.Button], args);

            // Call any button released
            if (_onButtonReleased.ContainsKey(Mouse.Button.ButtonCount))
                CallEventList<MouseButtonEventArgs>(_onButtonReleased[Mouse.Button.ButtonCount], args);
        }
        internal void OnWheelMoved(MouseWheelEventArgs args)
        {
            // Call wheel moved
            CallEventList<MouseWheelEventArgs>(_onWheelMoved, args);
        }
        internal void OnMoved(MouseMoveEventArgs args)
        {
            // Call mouse moved
            CallEventList<MouseMoveEventArgs>(_onMoved, args);
        }

        private static void CallEventList<T>(List<DeleHandler<T>> events, T args)
            //where T : EventArgs
        {
            int length = events.Count;
            for (int i = 0; i < length; i++)
                events[i](args);
        }
    }
}
