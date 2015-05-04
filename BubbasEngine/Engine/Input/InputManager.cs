using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BubbasEngine.Engine.Input.Devices;
using BubbasEngine.Engine.Windows;

namespace BubbasEngine.Engine.Input
{
    /// <summary>
    /// Represents the state of a button or key
    /// </summary>
    public enum InputState
    {
        /// <summary>
        /// Button/Key is up
        /// </summary>
        Up,

        /// <summary>
        /// Button/Key was pressed (this tick)
        /// </summary>
        Pressed,

        /// <summary>
        /// Button/Key is down
        /// </summary>
        Down,

        /// <summary>
        /// Button/Key was released (this tick)
        /// </summary>
        Released
    }

    /// <summary>
    /// Handles input and input-devices
    /// </summary>
    public class InputManager
    {
        // Private
        private Devices.Keyboard _keyboard;
        private Mouse _mouse;
        private List<JoystickDevice> _joysticks;

        private InputSettings _settings;
        private GameWindow _window;

        private Action _beginFrame;

        // Internal
        internal InputSettings Settings
        { get { return _settings; } }

        // Public devices
        public Mouse Mouse
        { get { return _mouse; } }
        public Devices.Keyboard Keyboard
        { get { return _keyboard; } }
        public ReadOnlyCollection<JoystickDevice> Joysticks
        { get; private set; }
        
        // Constructor(s)
        internal InputManager(InputSettingsArgs args)
        {
            // Create devices
            _keyboard = new Keyboard();
            _mouse = new Mouse();

            // Create container for joysticks
            _joysticks = new List<JoystickDevice>();
            Joysticks = _joysticks.AsReadOnly();

            // Settings
            _settings = new InputSettings(args);
        }

        // Handle Window
        internal void SetWindow(GameWindow window)
        {
            // Remove previous window
            if (_window != null)
                RemoveWindow();

            // 
            _window = window;

            // 
            _keyboard.ApplyWindow(_window);
            _mouse.ApplyWindow(_window);
        }
        internal void RemoveWindow()
        {
            // 
            _keyboard.RemoveWindow(_window);
            _mouse.RemoveWindow(_window);

            //
            _window = null;
        }

        // Devies
        private void UpdateDevices()
        {
            // Check if devices are 
            bool any = (_settings.FocusedInputOnly) ? _window.Focused : true;
            bool gamepad = (any)  ? ((_settings.FocusedGamePadInputOnly) ? _window.Focused : true) : false;
            bool keyboard = (any) ? ((_settings.FocusedKeyoardInputOnly) ? _window.Focused : true) : false;
            bool mouse = (any)    ? ((_settings.FocusedMouseInputOnly)   ? _window.Focused : true) : false;
            
            // Update standard devices
            _keyboard.Update(keyboard);
            _mouse.Update(mouse);

            // Update joysticks/gamepads

        }

        // BeginFrame
        internal void BeginFrame()
        {
            // Apply one-time-actions
            if (_beginFrame != null)
            {
                _beginFrame();
                _beginFrame = null;
            }

            //
            _keyboard.BeginFrame();
            _mouse.BeginFrame();


        }

        // Update
        internal void Update()
        {
            // Update devices
            UpdateDevices();
        }
    }
}
