using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;
using System.IO;

namespace BubbasEngine.Engine.Graphics.Drawables
{
    public class BSprite : IRenderable
    {
        // Private
        private Sprite _sprite;
        private RenderStates _state;
        private int _depth;

        // Public
        public Color Color
        { get { return _sprite.Color; } set { _sprite.Color = value; } }
        public Vector2f Origin
        { get { return _sprite.Origin; } set { _sprite.Origin = value; } }
        public Vector2f Position
        { get { return _sprite.Position; } set { _sprite.Position = value; } }
        public float Rotation
        { get { return _sprite.Rotation; } set { _sprite.Rotation = value; } }
        public Vector2f Scale
        { get { return _sprite.Scale; } set { _sprite.Scale = value; } }
        public Shader Shader
        { get { return _state.Shader; } }
        public Texture Texture
        { get { return _sprite.Texture; } set { _sprite.Texture = value; } }
        public Transform Transform
        { get { return _state.Transform; } set { _state.Transform = value; } }

        public int Depth
        { get { return _depth; } set { _depth = value; } }

        // Constructor(s)
        public BSprite()
        {
            _sprite = new Sprite();
            _state = RenderStates.Default;
        }
        public BSprite(Texture texture)
        {
            _sprite = new Sprite(texture);
            _state = RenderStates.Default;
        }
        public BSprite(Texture texture, Shader shader)
        {
            _sprite = new Sprite(texture);
            _state = new RenderStates(shader);
        }

        //
        public FloatRect GetLocalBounds()
        {
            return _sprite.GetLocalBounds();
        }
        public FloatRect GetGlobalBounds()
        {
            return _sprite.GetGlobalBounds();
        }

        // 
        public void SetShader(Shader shader)
        {
            _state.Shader = shader;
        }
        public void RemoveShader()
        {
            
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
            _sprite.Draw(target, _state);
        }
    }
}
