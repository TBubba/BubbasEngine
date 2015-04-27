using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;
using SFML.Graphics;

namespace BubbasEngine.Engine.Graphics.Drawables.Shapes
{
    public class BRectangleShape : BShape
    {
        // Public
        public Vector2f Size
        { get { return GetShape().Size; } set { GetShape().Size = value; } }
        public uint PointCount
        { get { return GetShape().GetPointCount(); } }

        // Constructor(s)
        public BRectangleShape(BRectangleShape copy)
        {
            _shape = new RectangleShape(copy.GetShape());
            _state = RenderStates.Default;
        }
        public BRectangleShape(Vector2f size)
        {
            _shape = new RectangleShape(size);
            _state = RenderStates.Default;
        }

        //
        private RectangleShape GetShape()
        {
            return (RectangleShape)_shape;
        }

        //
        public Vector2f GetPoint(uint index)
        {
            return GetShape().GetPoint(index);
        }
    }
}
