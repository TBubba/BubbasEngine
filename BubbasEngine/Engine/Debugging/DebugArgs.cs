using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BubbasEngine.Engine.Debugging
{
    public class DebugArgs
    {
        public bool Activated;

        public string DebugFontPath;
        public int Layer;

        // Constructor(s)
        public DebugArgs()
        {
            Activated = false;

            DebugFontPath = "";
            Layer = -1;
        }
        public DebugArgs(DebugArgs args)
        {
            Activated = args.Activated;
            DebugFontPath = args.DebugFontPath;
        }
    }
}
