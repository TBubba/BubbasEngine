using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BubbasEngine.Engine.Graphics.Drawables
{
    /// <summary>
    /// Interface for every object that can be drawn to a render target
    /// </summary>
    public interface IDrawable
    {
        /// <summary>
        /// Get the depth of the object
        /// </summary>
        /// <returns></returns>
        float GetDepth();

        /// <summmary>
        /// Draw the object to a render target
        ///
        /// This is a pure virtual function that has to be implemented
        /// by the derived class to define how the drawable should be
        /// drawn.
        /// </summmary>
        /// <param name="target">Render target to draw to</param>
        /// <param name="states">Current render states</param>
        void Draw(IRenderTarget target, RenderStates states);

        /// <summmary>
        /// Draw the object to a render target
        ///
        /// This is a pure virtual function that has to be implemented
        /// by the derived class to define how the drawable should be
        /// drawn.
        /// </summmary>
        /// <param name="target">Render target to draw to</param>
        void Draw(IRenderTarget target);
    }
}
