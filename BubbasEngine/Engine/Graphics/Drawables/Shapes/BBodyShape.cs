using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using BubbasEngine.Engine.Physics.Dynamics;
using BubbasEngine.Engine.Physics.Common;
using SFML.Window;
using BubbasEngine.Engine.Physics.Collision.Shapes;

namespace BubbasEngine.Engine.Graphics.Drawables.Shapes
{
    public class BBodyShape : IRenderable
    {
        // Private
        private List<BShape> _shapes;

        private int _depth;
        private Color _fillColor;

        //
        public int Depth
        { get { return _depth; } set { _depth = value; } }
        public Color FillColor
        { get { return _fillColor; } set { SetFillColor(value); } }

        // Constrcutor(s)
        public BBodyShape()
        {
            _shapes = new List<BShape>();
        }

        //
        public void SetShape(Body body)
        {
            SetShape(body, Vector2.One);
        }
        public void SetShape(Body body, float scale)
        {
            SetShape(body, Vector2.One * scale);
        }
        public void SetShape(Body body, Vector2 scale)
        {
            //
            int fCount = body.FixtureList.Count;

            // Make sure the number of shapes is correct
            if (fCount > _shapes.Count)
            {
                for (int i = _shapes.Count; i < fCount; i++)
                    _shapes.Add(CreateConvexShape());
            }
            else if (fCount < _shapes.Count)
            {
                for (int i = _shapes.Count - 1; i >= fCount; i--)
                    _shapes.RemoveAt(i);
            }

            // Set shapes
            for (int i = 0; i < fCount; i++)
            {
                // Get type of shape to replicate (to graphical shape)
                Type shapeType = body.FixtureList[i].Shape.GetType();

                if (shapeType == typeof(PolygonShape)) // Plygonal shape
                {
                    // Make sure it's the right type of shape in the list
                    if (_shapes[i].GetType() != typeof(BConvexShape))
                        _shapes[i] = CreateConvexShape();
                    BConvexShape gShape = (BConvexShape)_shapes[i];

                    //
                    PolygonShape shape = (PolygonShape)body.FixtureList[i].Shape;
                    Vector2 center = shape.Normals.GetCentroid();

                    //
                    if (gShape.PointCount != (uint)shape.Vertices.Count)
                        gShape.SetPointCount((uint)shape.Vertices.Count);

                    //
                    BubbasEngine.Engine.Physics.Common.Transform xf;
                    body.GetTransform(out xf);
                    for (int j = 0; j < shape.Vertices.Count; j++)
                    {
                        //
                        Vector2 vector = (MathUtils.Mul(ref xf, shape.Vertices[j]) - center) * scale;
                        gShape.SetPoint((uint)j, new Vector2f(vector.X + center.X, vector.Y + center.Y));
                    }
                }
                else if (shapeType == typeof(BubbasEngine.Engine.Physics.Collision.Shapes.CircleShape)) // Circle
                {
                    // Make sure it's the right type of shape in the list
                    if (_shapes[i].GetType() != typeof(BCircleShape))
                        _shapes[i] = CreateCircleShape();
                    BCircleShape gShape = (BCircleShape)_shapes[i];

                    //
                    BubbasEngine.Engine.Physics.Collision.Shapes.CircleShape shape = (BubbasEngine.Engine.Physics.Collision.Shapes.CircleShape)body.FixtureList[i].Shape;

                    //
                    BubbasEngine.Engine.Physics.Common.Transform xf;
                    body.GetTransform(out xf);

                    Vector2 pos = (MathUtils.Mul(ref xf, shape._position) - new Vector2(shape._radius, shape._radius)) * scale;

                    gShape.Position = new Vector2f(pos.X, pos.Y);
                    gShape.Radius = shape._radius;
                    gShape.Scale = new Vector2f(scale.X, scale.Y);
                }
                else
                {
                    // Shape type not supported
                    throw new Exception(string.Format("Shape type not supported for transformation to SFML-shape (ShapeType: {0}, Error in: {1})",
                                                      shapeType.ToString(),
                                                      GetType().ToString()));
                }
            }
        }

        //
        private void SetFillColor(Color color)
        {
            _fillColor = color;

            int length = _shapes.Count;
            for (int i = 0; i < length; i++)
                _shapes[i].FillColor = color;
        }

        private BConvexShape CreateConvexShape()
        {
            return new BConvexShape(0u) { FillColor = _fillColor };
        }
        private BCircleShape CreateCircleShape()
        {
            return new BCircleShape(0f) { FillColor = _fillColor };
        }

        //
        public int GetDepth()
        {
            return _depth;
        }
        public void Draw(RenderTarget target)
        {
            // Draw
            int length = _shapes.Count;
            for (int i = 0; i < length; i++)
                _shapes[i].Draw(target);
        }
    }
}
