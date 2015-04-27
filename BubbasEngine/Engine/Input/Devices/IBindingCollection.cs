using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BubbasEngine.Engine.Input.Devices
{
    public interface IBindingCollection
    {
        void Apply(InputDevice device);
        void Remove(InputDevice device);
    }
}
