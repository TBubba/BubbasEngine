using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace BubbasEngine.Engine.Generic
{
    /// <summary>
    /// Vector2f is an utility class for manipulating 2 dimensional
    /// vectors with float components
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2f
    {
        public static readonly Vector2f One = new Vector2f(1f, 1f);

        /// <summary>
        /// Construct the vector from its coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Vector2f(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Construct the vector from its coordinates
        /// </summary>
        /// <param name="c">Value of both coordinates</param>
        public Vector2f(float c)
        {
            X = c;
            Y = c;
        }

        /// <summary>
        /// Construct the vector from another vectors coordinates
        /// </summary>
        /// <param name="vec">Vector coordinates</param>
        public Vector2f(Vector2i vec)
        {
            X = (float)vec.X;
            Y = (float)vec.Y;
        }

        /// <summary>
        /// Construct the vector from another vectors coordinates
        /// </summary>
        /// <param name="vec">Vector coordinates</param>
        public Vector2f(Vector2u vec)
        {
            X = (float)vec.X;
            Y = (float)vec.Y;
        }

        /// <summary>
        /// Operator - overload ; returns the opposite of a vector
        /// </summary>
        /// <param name="v">Vector to negate</param>
        /// <returns>-v</returns>
        public static Vector2f operator -(Vector2f v)
        {
            return new Vector2f(-v.X, -v.Y);
        }

        /// <summary>
        /// Operator - overload ; subtracts two vectors
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>v1 - v2</returns>
        public static Vector2f operator -(Vector2f v1, Vector2f v2)
        {
            return new Vector2f(v1.X - v2.X, v1.Y - v2.Y);
        }

        // Murgalurg
        public static Vector2f operator -(Vector2f v1, float xy)
        {
            return new Vector2f(v1.X - xy, v1.Y - xy);
        }

        /// <summary>
        /// Operator + overload ; add two vectors
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>v1 + v2</returns>
        public static Vector2f operator +(Vector2f v1, Vector2f v2)
        {
            return new Vector2f(v1.X + v2.X, v1.Y + v2.Y);
        }

        // Murgalurg
        public static Vector2f operator +(Vector2f v1, float xy)
        {
            return new Vector2f(v1.X + xy, v1.Y + xy);
        }

        /// <summary>
        /// Operator * overload ; multiply a vector by a scalar value
        /// </summary>
        /// <param name="v">Vector</param>
        /// <param name="x">Scalar value</param>
        /// <returns>v * x</returns>
        public static Vector2f operator *(Vector2f v, float x)
        {
            return new Vector2f(v.X * x, v.Y * x);
        }

        /// <summary>
        /// Operator * overload ; multiply a scalar value by a vector
        /// </summary>
        /// <param name="x">Scalar value</param>
        /// <param name="v">Vector</param>
        /// <returns>x * v</returns>
        public static Vector2f operator *(float x, Vector2f v)
        {
            return new Vector2f(v.X * x, v.Y * x);
        }

        // TODO
        public static Vector2f operator *(Vector2f v, Vector2f v2)
        {
            return new Vector2f(v.X * v2.X, v.Y * v2.Y);
        }

        /// <summary>
        /// Operator / overload ; divide a vector by a scalar value
        /// </summary>
        /// <param name="v">Vector</param>
        /// <param name="x">Scalar value</param>
        /// <returns>v / x</returns>
        public static Vector2f operator /(Vector2f v, float x)
        {
            return new Vector2f(v.X / x, v.Y / x);
        }

        // TODO
        public static Vector2f operator /(Vector2f v, Vector2f v2)
        {
            return new Vector2f(v.X / v2.X, v.Y / v2.Y);
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return "[Vector2f]" +
                    " X(" + X + ")" +
                    " Y(" + Y + ")";
        }

        /// <summary>X (horizontal) component of the vector</summary>
        public float X;

        /// <summary>Y (vertical) component of the vector</summary>
        public float Y;
    }
}
