using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BubbasEngine.SFMLWindow
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Give access to the real-time state of the mouse
    /// </summary>
    ////////////////////////////////////////////////////////////
    public class Mouse
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Mouse buttons
        /// </summary>
        ////////////////////////////////////////////////////////////
        public enum Button
        {
            /// <summary>The left mouse button</summary>
            Left,

            /// <summary>The right mouse button</summary>
            Right,

            /// <summary>The middle (wheel) mouse button</summary>
            Middle,

            /// <summary>The first extra mouse button</summary>
            XButton1,

            /// <summary>The second extra mouse button</summary>
            XButton2,

            /// <summary>Keep last -- the total number of mouse buttons</summary>
            ButtonCount
        };
    }
}
