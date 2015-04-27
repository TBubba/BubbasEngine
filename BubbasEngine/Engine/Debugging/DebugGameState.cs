using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.Graphics.Drawables;
using SFML.Graphics;
using SFML.Window;
using BubbasEngine.Engine.Input.Devices;

namespace BubbasEngine.Engine.Debugging
{
    class DebugGameState : GameStates.GameState
    {
        // Private
        private bool _hide;

        private BText _topLeft;
        private BSprite _cursor;
        private BSprite _cursorScaled;

        private bool _resoursesMissing;

        private KeyboardBindingCollection _keys;
        private MouseBindingCollection _mouse;

        private DebugArgs _args;

        // Constructor(s)
        internal DebugGameState(DebugArgs args)
        {
            // Copy args
            _args = new DebugArgs(args);

            // Keybindings
            _keys = new KeyboardBindingCollection();
            _keys.AddOnPressed(Keyboard.Key.F2,
                new KeyboardBinding(new KeyboardInputDele(delegate
                    {
                        ToggleShow();
                    })));

            _mouse = new MouseBindingCollection();
            _mouse.AddOnMoved(new MouseMoveBinding((x, y) =>
                {
                    //_cursor.Position = new Vector2f(x, y);
                    //_cursorScaled.Position = new Vector2f(x, y) / _graphics.Scale;
                }));
        }

        //
        public override void Initialize()
        {

        }

        public override void LoadContent()
        {
            /*
            //
            string fontPath = _args.DebugFontPath;
            string pixelPath = Content.ContentManager.EnginePixelPath;

            bool anyFont = (fontPath != ""); // Don't activate text if no font is supposed to be loaded

            // Remember if any resource is missing
            _resoursesMissing = !anyFont;

            //
            if (anyFont)
                _content.RequestFont(fontPath, this);
            _content.RequestTexture(pixelPath, this);

            // Info text
            if (anyFont)
            {
                _topLeft = new BText(_content.GetFont(fontPath));
                _topLeft.CharacterSize = 12;
            }

            // Cursor sprite
            _cursor = new BSprite(_content.GetTexture(pixelPath));
            _cursor.Color = Color.Blue;

            // Cursor sprite
            _cursorScaled = new BSprite(_content.GetTexture(pixelPath));
            _cursorScaled.Color = Color.Red;
            */

            // Apply Keybindings
            _keys.Apply(_input.Keyboard);
            _mouse.Apply(_input.Mouse);
        }

        public override void BeginFrame()
        {

        }

        public override void Step()
        {
            // If there is any text to change
            if (_topLeft != null)
                _topLeft.Text = string.Format("TimeStep: {0}\n" +
                                              "TimeSinceStep: {1}\n" +
                                              "GoalFPS: {2}\n" +
                                              "Current FPS: {3}",
                                              _engine.Time.TimeSinceStep().ToString(),
                                              _engine.Time.LastFrameTime(),
                                              _engine.Time.GetGoalFPS(),
                                              "i dont fking know");
        }

        public override void Animate(float delta)
        {
        }

        public override void UnloadContent()
        {
            // Unload content
            //content.DEQUSET(this, @"intro\logo.png");

            // Remove Keybindings
            _keys.Remove(_input.Keyboard);
            _mouse.Remove(_input.Mouse);
        }

        //
        private void ToggleShow()
        {
            //
            if (_resoursesMissing)
            {
                GameConsole.WriteLine(string.Format("{0}: ", GetType().Name), GameConsole.MessageType.Error); // Debug
                return;
            }

            // Change value
            _hide = !_hide;
            _layer.Hide = _hide;
        }
    }
}
