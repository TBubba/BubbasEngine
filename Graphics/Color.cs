using System;
using System.Runtime.InteropServices;

namespace SFML
{
    namespace Graphics
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Utility class for manipulating 32-bits RGBA colors
        /// </summary>
        ////////////////////////////////////////////////////////////
        [StructLayout(LayoutKind.Sequential)]
        public struct Color
        {
            //
            public static Color Blend(Color c1, Color c2, float force)
            {
                // Clamp force between 0.0 and 1.0
                if (force > 1f) force = 1f;
                if (force < 0f) force = 0f;

                // Convert from 0 - 255 to 0.0 - 1.0
                float a, r, b, g;
                float c1A = (float)c1.A / 255f,
                      c1R = (float)c1.R / 255f,
                      c1G = (float)c1.G / 255f,
                      c1B = (float)c1.B / 255f,
                      c2A = (float)c2.A / 255f,
                      c2R = (float)c2.R / 255f,
                      c2G = (float)c2.G / 255f,
                      c2B = (float)c2.B / 255f;

                // Caluclate
                a = c1A + (c2A - c1A) * (1f - force);
                r = c1R + (c2R - c1R) * (1f - force);
                g = c1G + (c2G - c1G) * (1f - force);
                b = c1B + (c2B - c1B) * (1f - force);

                // Return
                return new Color((byte)(255f * r),
                                 (byte)(255f * g),
                                 (byte)(255f * b),
                                 (byte)(255f * a));
            }
            public static Color Blend(Color c1, Color c2)
            {
                // Convert from 0 - 255 to 0.0 - 1.0
                float a, r, b, g;
                float c1A = (float)c1.A / 255f,
                      c1R = (float)c1.R / 255f,
                      c1G = (float)c1.G / 255f,
                      c1B = (float)c1.B / 255f,
                      c2A = (float)c2.A / 255f,
                      c2R = (float)c2.R / 255f,
                      c2G = (float)c2.G / 255f,
                      c2B = (float)c2.B / 255f;

                // Caluclate
                a = 1f - (1f - c2A) * (1f - c1A); // Calculate new alpha
                r = c2R * c2A / a + c1R * c1A * (1f - c2A) / a;
                g = c2G * c2A / a + c1G * c1A * (1f - c2A) / a;
                b = c2B * c2A / a + c1B * c1A * (1f - c2A) / a;

                // Return
                return new Color((byte)(255f * r),
                                 (byte)(255f * g),
                                 (byte)(255f * b),
                                 (byte)(255f * a));
            }

            /// <summary>Red component of the color</summary>
            public byte R;

            /// <summary>Green component of the color</summary>
            public byte G;

            /// <summary>Blue component of the color</summary>
            public byte B;

            /// <summary>Alpha (transparent) component of the color</summary>
            public byte A;

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the color from its red, green and blue components
            /// </summary>
            /// <param name="red">Red component</param>
            /// <param name="green">Green component</param>
            /// <param name="blue">Blue component</param>
            ////////////////////////////////////////////////////////////
            public Color(byte red, byte green, byte blue) :
                this(red, green, blue, 255)
            {
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the color from its red, green, blue and alpha components
            /// </summary>
            /// <param name="red">Red component</param>
            /// <param name="green">Green component</param>
            /// <param name="blue">Blue component</param>
            /// <param name="alpha">Alpha (transparency) component</param>
            ////////////////////////////////////////////////////////////
            public Color(byte red, byte green, byte blue, byte alpha)
            {
                R = red;
                G = green;
                B = blue;
                A = alpha;
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the color from another
            /// </summary>
            /// <param name="color">Color to copy</param>
            ////////////////////////////////////////////////////////////
            public Color(Color color) :
                this(color.R, color.G, color.B, color.A)
            {
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Provide a string describing the object
            /// </summary>
            /// <returns>String description of the object</returns>
            ////////////////////////////////////////////////////////////
            public override string ToString()
            {
                return "[Color]" +
                       " R(" + R + ")" +
                       " G(" + G + ")" +
                       " B(" + B + ")" +
                       " A(" + A + ")";
            }


            // Operators (TODO: Add XML comments)
            public static Color operator -(Color c1, Color c2)
            {
                return new Color((byte)(c1.R - c2.R),
                                 (byte)(c1.G - c2.G),
                                 (byte)(c1.B - c2.B),
                                 (byte)(c1.A - c2.A));
            }
            public static Color operator +(Color c1, Color c2)
            {
                return new Color((byte)(c1.R + c2.R),
                                 (byte)(c1.G + c2.G),
                                 (byte)(c1.B + c2.B),
                                 (byte)(c1.A + c2.A));
            }
            public static Color operator *(Color c1, Color c2)
            {
                return new Color((byte)((double)c1.R * (double)c2.R),
                                 (byte)((double)c1.G * (double)c2.G),
                                 (byte)((double)c1.B * (double)c2.B),
                                 (byte)((double)c1.A * (double)c2.A));
            }
            public static Color operator /(Color c1, Color c2)
            {
                return new Color((byte)((double)c1.R / (double)c2.R),
                                 (byte)((double)c1.G / (double)c2.G),
                                 (byte)((double)c1.B / (double)c2.B),
                                 (byte)((double)c1.A / (double)c2.A));
            }


            /// <summary>Predefined black color</summary>
            public static readonly Color Black = new Color(0, 0, 0);

            /// <summary>Predefined dark gray color</summary>
            public static readonly Color DarkGray = new Color(137, 137, 137);

            /// <summary>Predefined light gray color</summary>
            public static readonly Color Gray = new Color(233, 233, 233);

            /// <summary>Predefined white color</summary>
            public static readonly Color White = new Color(255, 255, 255);

            /// <summary>Predefined red color</summary>
            public static readonly Color Red = new Color(255, 0, 0);

            /// <summary>Predefined green color</summary>
            public static readonly Color Green = new Color(0, 255, 0);

            /// <summary>Predefined blue color</summary>
            public static readonly Color Blue = new Color(0, 0, 255);

            /// <summary>Predefined yellow color</summary>
            public static readonly Color Yellow = new Color(255, 255, 0);

            /// <summary>Predefined orange color</summary>
            public static readonly Color Orange = new Color(255, 165, 0);

            /// <summary>Predefined magenta color</summary>
            public static readonly Color Magenta = new Color(255, 0, 255);

            /// <summary>Predefined cyan color</summary>
            public static readonly Color Cyan = new Color(0, 255, 255);

            /// <summary>Predefined (black) transparent color</summary>
            public static readonly Color Transparent = new Color(0, 0, 0, 0);
        }
    }
}
