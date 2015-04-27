using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;

namespace BubbasEngine.Engine.Graphics.Drawables
{
    public class BVertexArray : IRenderable
    {
        // Private
        private VertexArray _vertices;
        private RenderStates _state;
        private int _depth; 

        // Public
        public Vertex this[uint index]
        {
            get
            {
                if (index < _vertices.VertexCount)
                    return _vertices[index];
                throw new Exception(string.Format("Out of reach. (Index {0}, Count {1}, get)", index, _vertices.VertexCount));
            }
            set
            {
                if (index < _vertices.VertexCount)
                    _vertices[index] = value;
                else
                    throw new Exception(string.Format("Out of reach. (Index {0}, Count {1}, set)", index, _vertices.VertexCount));
            }
        }
        public uint Count
        { get { return _vertices.VertexCount; } }

        public Shader Shader
        { get { return _state.Shader; } }

        public int Depth
        { get { return _depth; } set { _depth = value; } }

        // Constructor(s)
        public BVertexArray()
        {
            _vertices = new VertexArray();
            _state = RenderStates.Default;
        }
        public BVertexArray(PrimitiveType type)
        {
            _vertices = new VertexArray(type);
            _state = RenderStates.Default;
        }
        public BVertexArray(PrimitiveType type, Shader shader)
        {
            _vertices = new VertexArray(type);
            _state = new RenderStates(shader);
        }
        public BVertexArray(PrimitiveType type, uint vertices)
        {
            _vertices = new VertexArray(type, vertices);
            _state = RenderStates.Default;
        }
        public BVertexArray(PrimitiveType type, uint vertices, Shader shader)
        {
            _vertices = new VertexArray(type, vertices);
            _state = new RenderStates(shader);
        }

        //
        public void SetShader(Shader shader)
        {
            _state.Shader = shader;
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
            _vertices.Draw(target, _state);
        }
    }
}
