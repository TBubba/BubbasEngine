using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace BubbasEngine.Engine.Generic
{
    /// <summary>
    /// Vector2u is an utility class for manipulating 2 dimensional
    /// vectors with unsigned integer components
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2u
    {
        /// <summary>
        /// Construct the vector from its coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Vector2u(uint x, uint y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Construct the vector from its coordinates
        /// </summary>
        /// <param name="c">Value of both coordinates</param>
        public Vector2u(uint c)
        {
            X = c;
            Y = c;
        }

        /// <summary>
        /// Construct the vector from another vectors coordinates
        /// </summary>
        /// <param name="vec">Vector coordinates</param>
        public Vector2u(Vector2f vec)
        {
            X = (uint)vec.X;
            Y = (uint)vec.Y;
        }

        /// <summary>
        /// Construct the vector from another vectors coordinates
        /// </summary>
        /// <param name="vec">Vector coordinates</param>
        public Vector2u(Vector2i vec)
        {
            X = (uint)vec.X;
            Y = (uint)vec.Y;
        }

        /// <summary>
        /// Operator - overload ; subtracts two vectors
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>v1 - v2</returns>
        public static Vector2u operator -(Vector2u v1, Vector2u v2)
        {
            return new Vector2u(v1.X - v2.X, v1.Y - v2.Y);
        }

        // Murgalurg
        public static Vector2u operator -(Vector2u v1, uint xy)
        {
            return new Vector2u(v1.X - xy, v1.Y - xy);
        }

        /// <summary>
        /// Operator + overload ; add two vectors
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>v1 + v2</returns>
        public static Vector2u operator +(Vector2u v1, Vector2u v2)
        {
            return new Vector2u(v1.X + v2.X, v1.Y + v2.Y);
        }

        // Murgalurg
        public static Vector2u operator +(Vector2u v1, uint xy)
        {
            return new Vector2u(v1.X + xy, v1.Y + xy);
        }

        /// <summary>
        /// Operator * overload ; multiply a vector by a scalar value
        /// </summary>
        /// <param name="v">Vector</param>
        /// <param name="x">Scalar value</param>
        /// <returns>v * x</returns>
        public static Vector2u operator *(Vector2u v, uint x)
        {
            return new Vector2u(v.X * x, v.Y * x);
        }

        /// <summary>
        /// Operator * overload ; multiply a scalar value by a vector
        /// </summary>
        /// <param name="x">Scalar value</param>
        /// <param name="v">Vector</param>
        /// <returns>x * v</returns>
        public static Vector2u operator *(uint x, Vector2u v)
        {
            return new Vector2u(v.X * x, v.Y * x);
        }

        /// <summary>
        /// Operator / overload ; divide a vector by a scalar value
        /// </summary>
        /// <param name="v">Vector</param>
        /// <param name="x">Scalar value</param>
        /// <returns>v / x</returns>
        public static Vector2u operator /(Vector2u v, uint x)
        {
            return new Vector2u(v.X / x, v.Y / x);
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return "[Vector2u]" +
                    " X(" + X + ")" +
                    " Y(" + Y + ")";
        }

        /// <summary>X (horizontal) component of the vector</summary>
        public uint X;

        /// <summary>Y (vertical) component of the vector</summary>
        public uint Y;
    }
}
