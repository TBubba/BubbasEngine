using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BubbasEngine.Engine.GameWorlds
{
    public class GameObject
    {
        // Private & Protected
        protected GameWorld _world
        { get; private set; }
        protected bool _active
        { get; private set; }

        // Public
        public bool Active
        { get { return _active; } internal set { _active = value; } }

        // Constructor(s)
        public GameObject()
        {
        }

        // World
        internal void AddToWorld(GameWorld world)
        {
            if (_world != null)
            {
                throw new Exception("added gameobject to another world without removing it from the first");
            }

            _world = world;
        }
        internal void RemoveFromWorld()
        {
            _world = null;
        }

        // ToString
        public override string ToString()
        {
            return "[" + GetType().Name + "]" +
                   " Active(" + _active + ")";
        }
    }
}
