using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;
using BubbasEngine.Engine.Input.Devices;
using BubbasEngine.Engine.Windows;
using BubbasEngine.Engine.GameStates;

namespace BubbasEngine.Engine.Input
{
    public class InputManager
    {
        // Private
        private KeyboardDevice _keyboard;
        private MouseDevice _mouse;

        private InputSettings _settings;
        private GameWindow _window;

        private Action _beginFrame;

        // Internal
        internal InputSettings Settings
        { get { return _settings; } }

        // Public devices
        public MouseDevice Mouse
        { get { return _mouse; } }
        public KeyboardDevice Keyboard
        { get { return _keyboard; } }
        
        // Constructor(s)
        internal InputManager(InputSettingsArgs args)
        {
            // Create devices
            _keyboard = new KeyboardDevice();
            _mouse = new MouseDevice();

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
