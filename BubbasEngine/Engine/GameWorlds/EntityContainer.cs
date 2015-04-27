using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BubbasEngine.Engine.GameWorlds
{
    public delegate void EntityEventDelegate(GameObject obj);

    public class EntityContainer
    {
        // Private
        private List<GameObject> _entities;
        private GameWorld _world;

        private Dictionary<Type, int> _counts;

        // Public
        public int Count
        { get { return _entities.Count; } }

        // Events
        public event EntityEventDelegate OnEntityAdded;
        public event EntityEventDelegate OnEntityRemoved;
        // Container
        public GameObject this[int index]
        {
            get { return _entities[index]; }
        }

        // Constructor(s)
        internal EntityContainer(GameWorld world)
        {
            // Create private container
            _entities = new List<GameObject>();

            // Counters
            _counts = new Dictionary<Type, int>();

            // Keep reference to world (bound to world)
            _world = world;
        }

        // Handle Entries
        public bool Add(GameObject entity)
        {
            // Abort if parameter is null
            if (entity == null)
            {
                GameConsole.WriteLine(string.Format("{0}: Tried to add a non-existing GameObject (obj = null)", GetType().Name), GameConsole.MessageType.Error); // Debug
                return false;
            }

            // Abort if entity is not found
            if (Contains(entity))
            {
                GameConsole.WriteLine(string.Format("{0}: Tried to add an entity that is already contained (Name {1})", GetType().Name, entity.GetType().Name), GameConsole.MessageType.Error); // Debug
                return false;
            }

            // Add entity to container
            _entities.Add(entity);

            // Update counter
            AddCount(entity.GetType());

            // Call event
            if (OnEntityAdded != null)
                OnEntityAdded(entity);

            // Success
            return true;
        }

        public bool Remove(GameObject entity)
        {
            // Abort if parameter is null
            if (entity == null)
            {
                GameConsole.WriteLine(string.Format("{0}: Tried to remove a non-existing GameObject (obj = null)", GetType().Name), GameConsole.MessageType.Error); // Debug
                return false;
            }

            // Abort if entity is not found
            if (!Contains(entity))
            {
                GameConsole.WriteLine(string.Format("{0}: Tried to remove a GameObject that is not in the container (Name {1})", GetType().Name, entity.GetType().Name), GameConsole.MessageType.Error); // Debug
                return false;
            }

            // Remove entity from container
            _entities.Remove(entity);

            // Call event
            if (OnEntityRemoved != null)
                OnEntityRemoved(entity);

            // Success
            return true;
        }

        public bool RemoveAt(int index)
        {
            // Abort if index is out of bounds
            if (index < 0 || index >= _entities.Count)
            {
                throw new Exception("index out of bounds");
                //return false; // in case of removal of the exception thrown above
            }

            // Remove entity from container
            GameObject entity = _entities[index];
            _entities.RemoveAt(index);

            // Call event
            if (OnEntityRemoved != null)
                OnEntityRemoved(entity);

            // Success
            return true;
        }

        public int RemoveRange(GameObject[] entities)
        {
            // Abort if the container is empty
            if (_entities.Count == 0)
            {
                GameConsole.WriteLine(string.Format("{0}: Tried to remove a range of GameObjects while none where contained", GetType().Name), GameConsole.MessageType.Error); // Debug
                return 0;
            }

            // Abort if parameter is invalid
            if (entities == null)
                throw new Exception("entities parameter cannot be null");

            // Queue range of entites for removal
            int count = 0;
            Action rem = new Action(delegate { });
            int length = entities.Length;
            for (int i = 0; i < length; i++)
            {
                // Keep entity
                GameObject entity = _entities[i];

                // Create action that removes selected entity
                rem += delegate
                {
                    // Remove entity from container
                    _entities.Remove(entity);

                    // Call event
                    if (OnEntityRemoved != null)
                        OnEntityRemoved(entity);
                };
            }

            // Remove range of entities
            rem();
            GameConsole.WriteLine(string.Format("{0}: Removed range of GameObjects (Count {1})", GetType().Name, count)); // Debug

            // Return
            return count;
        }
        public int RemoveRange(int start, int length)
        {
            // Abort if the container is empty
            if (_entities.Count == 0)
            {
                GameConsole.WriteLine(string.Format("{0}: Tried to remove a range of GameObjects while none where contained", GetType().Name), GameConsole.MessageType.Error); // Debug
                return 0;
            }

            if (start >= _entities.Count || start < 0)
                throw new Exception("start index out of bounds");

            if (length > _entities.Count || length < 0)
                throw new Exception("length out of bounds");

            // Queue range of entites for removal
            Action rem = new Action(delegate { });
            for (int i = start + length - 1; i >= start; i--)
            {
                // Keep entity and index
                int index = i;
                GameObject entity = _entities[index];

                // Create action that removes selected entity
                rem += delegate
                {
                    // Remove entity from container
                    _entities.RemoveAt(index);

                    // Call event
                    if (OnEntityRemoved != null)
                        OnEntityRemoved(entity);
                };
            }

            // Remove range of entities
            rem();
            GameConsole.WriteLine(string.Format("{0}: Removed range of GameObjects (From index {1} to {2}; Count {3})", GetType().Name, start, start + length, length)); // Debug

            // Return amount of entities removed
            return length;
        }

        public int RemoveAll()
        {
            // Abort if the container is empty
            if (_entities.Count == 0)
            {
                GameConsole.WriteLine(string.Format("{0}: Tried to remove all GameObjects while none were contained", GetType().Name), GameConsole.MessageType.Error); // Debug
                return 0;
            }

            // Queue all entites for removal
            Action rem = new Action(delegate { });
            int length = _entities.Count;
            for (int i = length - 1; i >= 0; i--)
            {
                // Keep entity and index
                int index = i;
                GameObject entity = _entities[index];

                // Create action that removes selected entity
                rem += delegate
                {
                    // Remove entity from container
                    _entities.RemoveAt(index);

                    // Call event
                    if (OnEntityRemoved != null)
                        OnEntityRemoved(entity);
                };
            }

            // Remove all entities
            rem();
            GameConsole.WriteLine(string.Format("{0}: Removed all GameObjects (Count {1})", GetType().Name, length)); // Debug

            // Return amount of entities removed
            return length;
        }

        //
        public bool Contains(GameObject entity)
        {
            // Compare paramter entity reference to every entity in the container
            int length = _entities.Count;
            for (int i = 0; i < length; i++)
            {
                if (ReferenceEquals(_entities[i], entity))
                    return true; // Success
            }

            // Failed to find entity in the collection
            return false;
        }

        // Counters
        private void AddCount(Type type)
        {
            // Add entity to counter
            if (!_counts.ContainsKey(type))
                _counts.Add(type, 1);
            else
                _counts[type] += 1;
        }
        private void RemoveCount(Type type)
        {
            // Remove entity from counter
            _counts[type] -= 1;

            if (_counts[type] <= 0)
                _counts.Remove(type);
        }

        public int CountType(Type type)
        {
            if (!_counts.ContainsKey(type))
                return 0;
            return _counts[type];
        }
    }
}
