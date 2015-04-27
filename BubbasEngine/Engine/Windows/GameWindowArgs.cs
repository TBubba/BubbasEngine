using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;

namespace BubbasEngine.Engine.Windows
{
    public class GameWindowArgs
    {
        public bool CreateWindow;

        // New window
        public uint WindowWidth;
        public uint WindowHeight;

        public string WindowTitle;
        public Styles WindowStyle;

        // Existing window
        public bool WaitForWindow;
        public IntPtr WindowPointer;

        // Constructor(s)
        public GameWindowArgs()
        {
            CreateWindow = true;

            WindowWidth = 1280;
            WindowHeight = 720;

            WindowTitle = "Game";
            WindowStyle = Styles.Default;
            
            WaitForWindow = true;
        }
        public GameWindowArgs(GameWindowArgs args)
        {
            CreateWindow = args.CreateWindow;

            WindowWidth = args.WindowWidth;
            WindowHeight = args.WindowHeight;

            WindowTitle = args.WindowTitle;
            WindowStyle = args.WindowStyle;

            WaitForWindow = args.WaitForWindow;
            WindowPointer = args.WindowPointer;
        }
    }
}
