using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;
using SFML.Graphics;

namespace BubbasEngine.Engine.Graphics.Drawables
{
    public class BText : IRenderable
    {
        // Private
        private Text _text;
        private RenderStates _state;
        private int _depth;

        // Public
        public uint CharacterSize
        { get { return _text.CharacterSize; } set { _text.CharacterSize = value; } }
        public Color Color
        { get { return _text.Color; } set { _text.Color = value; } }
        public Font Font
        { get { return _text.Font; } set { _text.Font = value; } }
        public Vector2f Origin
        { get { return _text.Origin; } set { _text.Origin = value; } }
        public Vector2f Position
        { get { return _text.Position; } set { _text.Position = value; } }
        public float Rotation
        { get { return _text.Rotation; } set { _text.Rotation = value; } }
        public Vector2f Scale
        { get { return _text.Scale; } set { _text.Scale = value; } }
        public string Text
        { get { return _text.DisplayedString; } set { _text.DisplayedString = value; } }

        public int Depth
        { get { return _depth; } set { _depth = value; } }

        // Constructor(s)
        public BText()
        {
            _text = new Text();
            _state = RenderStates.Default;
        }
        public BText(Font font)
        {
            _text = new Text("", font);
            _state = RenderStates.Default;
        }
        public BText(string text, Font font)
        {
            _text = new Text(text, font);
            _state = RenderStates.Default;
        }

        //
        public FloatRect GetLocalBounds()
        {
            return _text.GetLocalBounds();
        }
        public FloatRect GetGlobalBounds()
        {
            return _text.GetGlobalBounds();
        }

        // Depth
        public int GetDepth()
        {
            return _depth;
        }

        // Draw
        public void Draw(RenderTarget target)
        {
            // Draw
            _text.Draw(target, _state);
        }
    }
}
