using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;

namespace BubbasEngine.Engine.Input.Devices
{
    public class KeyboardBindingCollection
    {
        // Private
        private Dictionary<Keyboard.Key, List<KeyboardBinding>> _onKeyPressed;
        private Dictionary<Keyboard.Key, List<KeyboardBinding>> _onKeyReleased;

        // Constructor(s)
        public KeyboardBindingCollection()
        {
            _onKeyPressed = new Dictionary<Keyboard.Key, List<KeyboardBinding>>();
            _onKeyReleased = new Dictionary<Keyboard.Key, List<KeyboardBinding>>();
        }

        // Add
        public void AddOnPressed(Keyboard.Key key, KeyboardBinding bind)
        {
            // Create if it doesnt exist
            if (!_onKeyPressed.ContainsKey(key))
                _onKeyPressed.Add(key, new List<KeyboardBinding>());

            // Add
            _onKeyPressed[key].Add(bind);
        }
        public void AddOnReleased(Keyboard.Key key, KeyboardBinding bind)
        {
            // Create if it doesnt exist
            if (!_onKeyReleased.ContainsKey(key))
                _onKeyReleased.Add(key, new List<KeyboardBinding>());

            // Add
            _onKeyReleased[key].Add(bind);
        }

        // Remove
        public void RemoveOnPressed(Keyboard.Key key, KeyboardBinding bind)
        {
            // Remove
            if (_onKeyPressed.ContainsKey(key))
            {
                if (!_onKeyPressed[key].Remove(bind))
                    GameConsole.WriteLine("errir pls"); // Debug
            }
            else
                GameConsole.WriteLine(string.Format(this.GetType().Name + ": Tried to remove keybinding from non-bound key (Button:{0}, OnPressed)", key), GameConsole.MessageType.Error); // Debug
        }
        public void RemoveOnReleased(Keyboard.Key key, KeyboardBinding bind)
        {
            // Remove
            if (_onKeyReleased.ContainsKey(key))
            {
                if (!_onKeyReleased[key].Remove(bind))
                    GameConsole.WriteLine("errir pls"); // Debug
            }
            else
                GameConsole.WriteLine(string.Format(this.GetType().Name + ": Tried to remove keybinding from non-bound key (Button:{0}, OnPressed)", key), GameConsole.MessageType.Error); // Debug
        }

        // Device
        public void Apply(KeyboardDevice device)
        {
            //
            foreach (KeyValuePair<Keyboard.Key, List<KeyboardBinding>> pair in _onKeyPressed)
            {
                int length = pair.Value.Count;
                for (int i = 0; i < length; i++)
                    ((KeyboardDevice)device).AddOnPressed(pair.Key, pair.Value[i]);
            }
            foreach (KeyValuePair<Keyboard.Key, List<KeyboardBinding>> pair in _onKeyReleased)
            {
                int length = pair.Value.Count;
                for (int i = 0; i < length; i++)
                    ((KeyboardDevice)device).AddOnReleased(pair.Key, pair.Value[i]);
            }
        }
        public void Remove(KeyboardDevice device)
        {
            //
            foreach (KeyValuePair<Keyboard.Key, List<KeyboardBinding>> pair in _onKeyPressed)
            {
                int length = pair.Value.Count;
                for (int i = 0; i < length; i++)
                    ((KeyboardDevice)device).RemoveOnPressed(pair.Key, pair.Value[i]);
            }
            foreach (KeyValuePair<Keyboard.Key, List<KeyboardBinding>> pair in _onKeyReleased)
            {
                int length = pair.Value.Count;
                for (int i = 0; i < length; i++)
                    ((KeyboardDevice)device).RemoveOnReleased(pair.Key, pair.Value[i]);
            }
        }
    }
}
