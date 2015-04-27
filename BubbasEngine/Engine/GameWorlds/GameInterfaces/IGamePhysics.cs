using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.Physics.Dynamics;

namespace BubbasEngine.Engine.GameWorlds.GameInterfaces
{
    public interface IGamePhysics
    {
        Body AddBody(PhysicsWorld world);
        Body GetBody();
    }
}
