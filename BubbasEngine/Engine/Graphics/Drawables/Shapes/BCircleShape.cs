using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace BubbasEngine.Engine.Graphics.Drawables.Shapes
{
    public class BCircleShape : BShape
    {
        // Public
        public float Radius
        { get { return GetShape().Radius; } set { GetShape().Radius = value; } }
        public uint PointCount
        { get { return GetShape().GetPointCount(); } set { GetShape().SetPointCount(value); } }

        // Constructor(s)
        public BCircleShape(BCircleShape copy)
        {
            _shape = new CircleShape(copy.GetShape());
            _state = RenderStates.Default;
        }
        public BCircleShape(float radius)
        {
            _shape = new CircleShape(radius);
            _state = RenderStates.Default;
        }
        public BCircleShape(float radius, uint pointCount)
        {
            _shape = new CircleShape(radius, pointCount);
            _state = RenderStates.Default;
        }

        //
        private CircleShape GetShape()
        {
            return (CircleShape)_shape;
        }

        //
        public Vector2f GetPoint(uint index)
        {
            return GetShape().GetPoint(index);
        }
    }
}
