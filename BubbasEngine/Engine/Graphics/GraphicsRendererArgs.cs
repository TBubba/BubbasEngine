using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BubbasEngine.Engine.Graphics
{
    public class GraphicsRendererArgs
    {
        //
        public uint ResolutionWidth;
        public uint ResolutionHeight;

        // Constructor(s)
        public GraphicsRendererArgs()
        {
            ResolutionWidth = 1280;
            ResolutionHeight = 720;
        }
        public GraphicsRendererArgs(GraphicsRendererArgs args)
        {
            ResolutionWidth = args.ResolutionWidth;
            ResolutionHeight = args.ResolutionHeight;
        }
    }
}
