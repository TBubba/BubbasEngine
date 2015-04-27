using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BubbasEngine.Engine.Input
{
    internal class InputSettings
    {
        private bool _focusedInputOnly; // Only takes input if GameWindow is focused
        private bool _focusedGamePadInputOnly; // Only takes gamepad input if the GameWindow is focused
        private bool _focusedKeyoardInputOnly; // Only takes keyboard input if the GameWindow is focused
        private bool _focusedMouseInputOnly; // Only takes mouse input if the GameWindow is focused

        public bool FocusedInputOnly
        { get { return _focusedInputOnly; } }
        public bool FocusedKeyoardInputOnly
        { get { return _focusedKeyoardInputOnly; } }
        public bool FocusedGamePadInputOnly
        { get { return _focusedGamePadInputOnly; } }
        public bool FocusedMouseInputOnly
        { get { return _focusedMouseInputOnly; } }

        internal InputSettings(InputSettingsArgs args)
        {
            _focusedInputOnly = args.FocusedInputOnly;
            _focusedGamePadInputOnly = args.FocusedGamePadInputOnly;
            _focusedKeyoardInputOnly = args.FocusedKeyoardInputOnly;
            _focusedMouseInputOnly = args.FocusedMouseInputOnly;
        }
    }
}
