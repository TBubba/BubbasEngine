using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BubbasEngine.Engine.Generic
{
    /// <summary>
    /// Dele(gate)Handle is effectively an EventHandler but without the "sender" parameter
    /// </summary>
    /// <typeparam name="T">The </typeparam>
    public delegate void DeleHandler<TArgs>(TArgs args);
}
