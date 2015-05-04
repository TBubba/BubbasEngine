using System;
using BubbasEngine.Engine.Input;
using BubbasEngine.Engine.Input.Devices;

namespace BubbasEngine.Engine.Windows
{
    /// <summary>
    /// Keyboard event parameters
    /// </summary>
    internal class SFMLKeyEventArgs : EventArgs
    {
        /// <summary>
        /// Construct the key arguments from a key event
        /// </summary>
        /// <param name="e">Key event</param>
        public SFMLKeyEventArgs(KeyEvent e)
        {
            Code    = e.Code;
            Alt     = e.Alt != 0;
            Control = e.Control != 0;
            Shift   = e.Shift != 0;
            System  = e.System != 0;
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return "[KeyEventArgs]" +
                    " Code(" + Code + ")" + 
                    " Alt(" + Alt + ")" + 
                    " Control(" + Control + ")" + 
                    " Shift(" + Shift + ")" +
                    " System(" + System + ")";
        }

        /// <summary>Code of the key (see KeyCode enum)</summary>
        public Keyboard.Key Code;

        /// <summary>Is the Alt modifier pressed?</summary>
        public bool Alt;

        /// <summary>Is the Control modifier pressed?</summary>
        public bool Control;

        /// <summary>Is the Shift modifier pressed?</summary>
        public bool Shift;

        /// <summary>Is the System modifier pressed?</summary>
        public bool System;
    }

    /// <summary>
    /// Text event parameters
    /// </summary>
    internal class TextEventArgs : EventArgs
    {
        /// <summary>
        /// Construct the text arguments from a text event
        /// </summary>
        /// <param name="e">Text event</param>
        public TextEventArgs(TextEvent e)
        {
            Unicode = Char.ConvertFromUtf32((int)e.Unicode);
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return "[TextEventArgs]" +
                    " Unicode(" + Unicode + ")";
        }

        /// <summary>UTF-16 value of the character</summary>
        public string Unicode;
    }

    /// <summary>
    /// Mouse move event parameters
    /// </summary>
    internal class SFMLMouseMoveEventArgs : EventArgs
    {
        /// <summary>
        /// Construct the mouse move arguments from a mouse move event
        /// </summary>
        /// <param name="e">Mouse move event</param>
        public SFMLMouseMoveEventArgs(MouseMoveEvent e)
        {
            X = e.X;
            Y = e.Y;
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return "[MouseMoveEventArgs]" +
                    " X(" + X + ")" +
                    " Y(" + Y + ")";
        }

        /// <summary>X coordinate of the mouse cursor</summary>
        public int X;

        /// <summary>Y coordinate of the mouse cursor</summary>
        public int Y;
    }

    /// <summary>
    /// Mouse buttons event parameters
    /// </summary>
    internal class SFMLMouseButtonEventArgs : EventArgs
    {
        /// <summary>
        /// Construct the mouse button arguments from a mouse button event
        /// </summary>
        /// <param name="e">Mouse button event</param>
        public SFMLMouseButtonEventArgs(MouseButtonEvent e)
        {
            Button = e.Button;
            X      = e.X;
            Y      = e.Y;
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return "[MouseButtonEventArgs]" +
                    " Button(" + Button + ")" +
                    " X(" + X + ")" +
                    " Y(" + Y + ")";
        }

        /// <summary>Code of the button (see MouseButton enum)</summary>
        public Mouse.Button Button;

        /// <summary>X coordinate of the mouse cursor</summary>
        public int X;

        /// <summary>Y coordinate of the mouse cursor</summary>
        public int Y;
    }

    /// <summary>
    /// Mouse wheel event parameters
    /// </summary>
    internal class SFMLMouseWheelEventArgs : EventArgs
    {
        /// <summary>
        /// Construct the mouse wheel arguments from a mouse wheel event
        /// </summary>
        /// <param name="e">Mouse wheel event</param>
        public SFMLMouseWheelEventArgs(MouseWheelEvent e)
        {
            Delta = e.Delta;
            X     = e.X;
            Y     = e.Y;
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return "[MouseWheelEventArgs]" +
                    " Delta(" + Delta + ")" +
                    " X(" + X + ")" +
                    " Y(" + Y + ")";
        }

        /// <summary>Scroll amount</summary>
        public int Delta;

        /// <summary>X coordinate of the mouse cursor</summary>
        public int X;

        /// <summary>Y coordinate of the mouse cursor</summary>
        public int Y;
    }

    /// <summary>
    /// Joystick axis move event parameters
    /// </summary>
    internal class JoystickMoveEventArgs : EventArgs
    {
        /// <summary>
        /// Construct the joystick move arguments from a joystick move event
        /// </summary>
        /// <param name="e">Joystick move event</param>
        public JoystickMoveEventArgs(JoystickMoveEvent e)
        {
            JoystickId = e.JoystickId;
            Axis       = e.Axis;
            Position   = e.Position;
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return "[JoystickMoveEventArgs]" +
                    " JoystickId(" + JoystickId + ")" +
                    " Axis(" + Axis + ")" +
                    " Position(" + Position + ")";
        }

        /// <summary>Index of the joystick which triggered the event</summary>
        public uint JoystickId;

        /// <summary>Joystick axis (see JoyAxis enum)</summary>
        public Joystick.Axis Axis;

        /// <summary>Current position of the axis</summary>
        public float Position;
    }

    /// <summary>
    /// Joystick buttons event parameters
    /// </summary>
    internal class JoystickButtonEventArgs : EventArgs
    {
        /// <summary>
        /// Construct the joystick button arguments from a joystick button event
        /// </summary>
        /// <param name="e">Joystick button event</param>
        public JoystickButtonEventArgs(JoystickButtonEvent e)
        {
            JoystickId = e.JoystickId;
            Button     = e.Button;
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return "[JoystickButtonEventArgs]" +
                    " JoystickId(" + JoystickId + ")" +
                    " Button(" + Button + ")";
        }

        /// <summary>Index of the joystick which triggered the event</summary>
        public uint JoystickId;

        /// <summary>Index of the button</summary>
        public uint Button;
    }

    /// <summary>
    /// Joystick connection/disconnection event parameters
    /// </summary>
    internal class JoystickConnectEventArgs : EventArgs
    {
        /// <summary>
        /// Construct the joystick connect arguments from a joystick connect event
        /// </summary>
        /// <param name="e">Joystick button event</param>
        public JoystickConnectEventArgs(JoystickConnectEvent e)
        {
            JoystickId = e.JoystickId;
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return "[JoystickConnectEventArgs]" +
                    " JoystickId(" + JoystickId + ")";
        }

        /// <summary>Index of the joystick which triggered the event</summary>
        public uint JoystickId;
    }

    /// <summary>
    /// Size event parameters
    /// </summary>
    internal class SizeEventArgs : EventArgs
    {
        /// <summary>
        /// Construct the size arguments from a size event
        /// </summary>
        /// <param name="e">Size event</param>
        public SizeEventArgs(SizeEvent e)
        {
            Width  = e.Width;
            Height = e.Height;
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return "[SizeEventArgs]" +
                    " Width(" + Width + ")" +
                    " Height(" + Height + ")";
        }

        /// <summary>New width of the window</summary>
        public uint Width;

        /// <summary>New height of the window</summary>
        public uint Height;
    }
}