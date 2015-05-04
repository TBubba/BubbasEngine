using System.Collections.Generic;
using BubbasEngine.Engine.Generic;

namespace BubbasEngine.Engine.Input.Devices
{
    public class KeyboardBindingCollection
    {
        /// <summary>
        /// Chains a MouseBindingCollection to another
        /// (calling events from "chain" will also call events from "link")
        /// (same as calling chain.Chain(link))
        /// </summary>
        /// <param name="chain">The collection that the "link" will be chained to</param>
        /// <param name="link">The collection that will be chained to the "chain"</param>
        /// <returns>"Chain" parameter</returns>
        public static KeyboardBindingCollection operator +(KeyboardBindingCollection chain, KeyboardBindingCollection link)
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
        public static KeyboardBindingCollection operator -(KeyboardBindingCollection chain, KeyboardBindingCollection link)
        {
            // Add link to chain
            chain.Dechain(link);

            // Return chain
            return chain;
        }

        // Private
        private Dictionary<Keyboard.Key, List<DeleHandler<KeyEventArgs>>> _onKeyPressed;
        private Dictionary<Keyboard.Key, List<DeleHandler<KeyEventArgs>>> _onKeyReleased;

        private event DeleHandler<KeyEventArgs> _keyPressed;
        private event DeleHandler<KeyEventArgs> _keyReleased;

        // Constructor(s)
        public KeyboardBindingCollection()
        {
            //
            _onKeyPressed = new Dictionary<Keyboard.Key, List<DeleHandler<KeyEventArgs>>>();
            _onKeyReleased = new Dictionary<Keyboard.Key, List<DeleHandler<KeyEventArgs>>>();

            //
            _keyPressed = OnPressed;
            _keyReleased = OnReleased;
        }

        // Add
        public void AddOnPressed(Keyboard.Key key, DeleHandler<KeyEventArgs> bind)
        {
            // Create if it doesnt exist
            if (!_onKeyPressed.ContainsKey(key))
                _onKeyPressed.Add(key, new List<DeleHandler<KeyEventArgs>>());

            // Add
            _onKeyPressed[key].Add(bind);
        }
        public void AddOnReleased(Keyboard.Key key, DeleHandler<KeyEventArgs> bind)
        {
            // Create if it doesnt exist
            if (!_onKeyReleased.ContainsKey(key))
                _onKeyReleased.Add(key, new List<DeleHandler<KeyEventArgs>>());

            // Add
            _onKeyReleased[key].Add(bind);
        }

        // Remove
        public void RemoveOnPressed(Keyboard.Key key, DeleHandler<KeyEventArgs> bind)
        {
            // Remove
            if (_onKeyPressed.ContainsKey(key))
            {
                if (!_onKeyPressed[key].Remove(bind))
                    GameConsole.WriteLine("errir pls"); // Debug
            }
            else
                GameConsole.WriteLine(string.Format(this.GetType().Name + ": Tried to remove keybinding from non-bound key (Key:{0}, OnPressed)", key), GameConsole.MessageType.Error); // Debug
        }
        public void RemoveOnReleased(Keyboard.Key key, DeleHandler<KeyEventArgs> bind)
        {
            // Remove
            if (_onKeyReleased.ContainsKey(key))
            {
                if (!_onKeyReleased[key].Remove(bind))
                    GameConsole.WriteLine("errir pls"); // Debug
            }
            else
                GameConsole.WriteLine(string.Format(this.GetType().Name + ": Tried to remove keybinding from non-bound key (Key:{0}, OnReleased)", key), GameConsole.MessageType.Error); // Debug
        }

        // Chain
        public void Chain(KeyboardBindingCollection bindings)
        {
            // Chain bindings
            _keyPressed += bindings._keyPressed;
            _keyReleased += bindings._keyReleased;
        }
        public void Dechain(KeyboardBindingCollection bindings)
        {
            // Dechain bindings
            _keyPressed -= bindings._keyPressed;
            _keyReleased -= bindings._keyReleased;
        }

        // Call
        internal void OnPressed(KeyEventArgs args)
        {
            // Call key pressed
            if (_onKeyPressed.ContainsKey(args.Key))
                CallEventList<KeyEventArgs>(_onKeyPressed[args.Key], args);

            // Call any key pressed
            if (_onKeyPressed.ContainsKey(Keyboard.Key.KeyCount))
                CallEventList<KeyEventArgs>(_onKeyPressed[Keyboard.Key.KeyCount], args);
        }
        internal void OnReleased(KeyEventArgs args)
        {
            // Call key released
            if (_onKeyReleased.ContainsKey(args.Key))
                CallEventList<KeyEventArgs>(_onKeyReleased[args.Key], args);

            // Call any key released
            if (_onKeyReleased.ContainsKey(Keyboard.Key.KeyCount))
                CallEventList<KeyEventArgs>(_onKeyReleased[Keyboard.Key.KeyCount], args);
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
