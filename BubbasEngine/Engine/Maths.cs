using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;

namespace BubbasEngine.Engine
{
    public static class Maths
    {
        public static float Pi = 3.14159265f;
        public static float Tau = Pi * 2;

        public static Vector2f Normalize(Vector2f vector)
        {
            float distance = (float)Math.Sqrt((double)vector.X * (double)vector.X + (double)vector.Y * (double)vector.Y);
            return new Vector2f(vector.X / distance, vector.Y / distance);
        }
    }
}
