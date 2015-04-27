using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace BubbasEngine.Engine.Graphics.Drawables
{
    public interface IRenderable
    {
        // Depth
        int GetDepth();

        // Draw
        void Draw(RenderTarget target);
    }
}
