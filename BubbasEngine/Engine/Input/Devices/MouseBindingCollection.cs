using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;

namespace BubbasEngine.Engine.Input.Devices
{
    public class MouseBindingCollection
    {
        // Private
        private Dictionary<Mouse.Button, List<MouseButtonBinding>> _onButtonPressed;
        private Dictionary<Mouse.Button, List<MouseButtonBinding>> _onButtonReleased;
        private List<MouseWheelBinding> _onWheelMoved;
        private List<MouseMoveBinding> _onMoved;

        // Constructor(s)
        public MouseBindingCollection()
        {
            _onButtonPressed = new Dictionary<Mouse.Button, List<MouseButtonBinding>>();
            _onButtonReleased = new Dictionary<Mouse.Button, List<MouseButtonBinding>>();
            _onWheelMoved = new List<MouseWheelBinding>();
            _onMoved = new List<MouseMoveBinding>();
        }

        // Add
        public void AddOnPressed(Mouse.Button button, MouseButtonBinding bind)
        {
            // Create if it doesnt exist
            if (!_onButtonPressed.ContainsKey(button))
                _onButtonPressed.Add(button, new List<MouseButtonBinding>());

            // Add
            _onButtonPressed[button].Add(bind);
        }
        public void AddOnReleased(Mouse.Button button, MouseButtonBinding bind)
        {
            // Create if it doesnt exist
            if (!_onButtonReleased.ContainsKey(button))
                _onButtonReleased.Add(button, new List<MouseButtonBinding>());

            // Add
            _onButtonReleased[button].Add(bind);
        }
        public void AddOnWheelChanged(MouseWheelBinding bind)
        {
            // Add
            _onWheelMoved.Add(bind);
        }
        public void AddOnMoved(MouseMoveBinding bind)
        {
            // Add
            _onMoved.Add(bind);
        }

        // Remove
        public void RemoveOnPressed(Mouse.Button button, MouseButtonBinding bind)
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
        }
        public void RemoveOnReleased(Mouse.Button button, MouseButtonBinding bind)
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
        }
        public void RemoveOnWheelChanged(MouseWheelBinding bind)
        {
            // Remove
            if (_onWheelMoved.Contains(bind))
            {
                _onWheelMoved.Remove(bind);
            }
            else
                GameConsole.WriteLine(string.Format("InputMouse: Tried to remove non-exsiting keybinding (OnWheelChanged)"), GameConsole.MessageType.Error); // Debug
        }
        public void RemoveOnMoved(MouseMoveBinding bind)
        {
            // Remove
            if (_onMoved.Contains(bind))
            {
                _onMoved.Remove(bind);
            }
            else
                GameConsole.WriteLine(string.Format("InputMouse: Tried to remove non-exsiting keybinding (OnMoved)"), GameConsole.MessageType.Error); // Debug
        }

        // Device
        public void Apply(MouseDevice device)
        {
            // Pressed
            foreach (KeyValuePair<Mouse.Button, List<MouseButtonBinding>> pair in _onButtonPressed)
            {
                int length = pair.Value.Count;
                for (int i = 0; i < length; i++)
                    device.AddOnPressed(pair.Key, pair.Value[i]);
            }

            // Released
            foreach (KeyValuePair<Mouse.Button, List<MouseButtonBinding>> pair in _onButtonReleased)
            {
                int length = pair.Value.Count;
                for (int i = 0; i < length; i++)
                    device.AddOnReleased(pair.Key, pair.Value[i]);
            }

            // Wheel
            int count = _onWheelMoved.Count;
            for (int i = 0; i < count; i++)
            {
                device.AddOnWheelChanged(_onWheelMoved[i]);
            }

            // Move
            count = _onMoved.Count;
            for (int i = 0; i < count; i++)
            {
                device.AddOnMoved(_onMoved[i]);
            }
        }
        public void Remove(MouseDevice device)
        {
            // Pressed
            foreach (KeyValuePair<Mouse.Button, List<MouseButtonBinding>> pair in _onButtonPressed)
            {
                int length = pair.Value.Count;
                for (int i = 0; i < length; i++)
                    device.RemoveOnPressed(pair.Key, pair.Value[i]);
            }

            // Released
            foreach (KeyValuePair<Mouse.Button, List<MouseButtonBinding>> pair in _onButtonReleased)
            {
                int length = pair.Value.Count;
                for (int i = 0; i < length; i++)
                    device.RemoveOnReleased(pair.Key, pair.Value[i]);
            }

            // Wheel
            int count = _onWheelMoved.Count;
            for (int i = 0; i < count; i++)
            {
                device.RemoveOnWheelChanged(_onWheelMoved[i]);
            }

            // Move
            count = _onMoved.Count;
            for (int i = 0; i < count; i++)
            {
                device.RemoveOnMoved(_onMoved[i]);
            }
        }
    }
}
