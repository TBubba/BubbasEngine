using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace BubbasEngine.Engine.Graphics.Drawables.Shapes
{
    public abstract class BShape : IRenderable
    {
        // Protected
        protected Shape _shape;
        protected RenderStates _state;
        protected int _depth;

        // Public
        public int Depth
        { get { return _depth; } set { _depth = value; } }

        // Public
        public Color FillColor
        { get { return _shape.FillColor; } set { _shape.FillColor = value; } }
        public Vector2f Origin
        { get { return _shape.Origin; } set { _shape.Origin = value; } }
        public Color OutlineColor
        { get { return _shape.OutlineColor; } set { _shape.OutlineColor = value; } }
        public float OutlineThickness
        { get { return _shape.OutlineThickness; } set { _shape.OutlineThickness = value; } }
        public Vector2f Position
        { get { return _shape.Position; } set { _shape.Position = value; } }
        public float Rotation
        { get { return _shape.Rotation; } set { _shape.Rotation = value; } }
        public Vector2f Scale
        { get { return _shape.Scale; } set { _shape.Scale = value; } }
        public Shader Shader
        { get { return _state.Shader; } }
        public Texture Texture
        { get { return _shape.Texture; } set { _shape.Texture = value; } }
        public Transform Transform
        { get { return _state.Transform; } set { _state.Transform = value; } }

        // Constructor(s)
        internal BShape()
        {
        }

        //
        public FloatRect GetLocalBounds()
        {
            return _shape.GetLocalBounds();
        }
        public FloatRect GetGlobalBounds()
        {
            return _shape.GetGlobalBounds();
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
            _shape.Draw(target, _state);
        }
    }
}
