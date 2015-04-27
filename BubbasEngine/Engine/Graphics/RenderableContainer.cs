using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.Graphics.Drawables;
using SFML.Graphics;

namespace BubbasEngine.Engine.Graphics
{
    public delegate void RenderableEventDelegate(IRenderable renderable);

    public class RenderableContainer
    {
        // Private
        private List<IRenderable> _renderables;

        // Public
        public int Count
        { get { return _renderables.Count; } }

        // Events
        public event RenderableEventDelegate OnRenderableAdded;
        public event RenderableEventDelegate OnRenderableRemoved;

        // Container
        public IRenderable this[int index]
        {
            get { return _renderables[index]; }
        }

        // Constructor(s)
        internal RenderableContainer()
        {
            _renderables = new List<IRenderable>();
        }

        // Render
        internal void Render(RenderTarget target)
        {
            // Sort (Lowest first)
            _renderables.Sort((x, y) => -x.GetDepth().CompareTo(y.GetDepth()));

            // Render all
            int length = _renderables.Count;
            for (int i = 0; i < length; i++)
                _renderables[i].Draw(target);
        }

        // Handle layers
        public bool Add(IRenderable renderable)
        {
            // Abort if parameter is null
            if (renderable == null)
            {
                GameConsole.WriteLine(string.Format("{0}: Tried to add a non-existing Renderable (renderable = null)", GetType().Name), GameConsole.MessageType.Error); // Debug
                return false;
            }

            // Abort if entity is not found
            if (Contains(renderable))
            {
                GameConsole.WriteLine(string.Format("{0}: Tried to add an renderable that is already contained (Name {1})", GetType().Name, renderable.GetType().Name), GameConsole.MessageType.Error); // Debug
                return false;
            }

            // Add entity to container
            _renderables.Add(renderable);

            // Success
            return true;
        }

        public bool Remove(IRenderable renderable)
        {
            // Abort if parameter is null
            if (renderable == null)
            {
                GameConsole.WriteLine("{0}: Tried to remove a non-existing Renderable (renderable = null)", GameConsole.MessageType.Error); // Debug
                return false;
            }

            // Abort if renderable is not found
            if (!Contains(renderable))
            {
                GameConsole.WriteLine(string.Format("{0}: Tried to remove a Renderable that is not in the container", GetType().Name), GameConsole.MessageType.Error); // Debug
                return false;
            }

            // Remove renderable from container
            _renderables.Remove(renderable);

            // Success
            return true;
        }

        public bool RemoveAt(int index)
        {
            // Abort if index is out of bounds
            if (index < 0 || index >= _renderables.Count)
            {
                throw new Exception("index out of bounds");
                //return false; // in case of removal of the exception thrown above
            }

            // Remove layer from container
            _renderables.RemoveAt(index);

            // Success
            return true;
        }

        public int RemoveAll()
        {
            // Abort if the container is empty
            if (_renderables.Count == 0)
            {
                GameConsole.WriteLine(string.Format("{0}: Tried to remove all GraphicsLayers while none were contained", GetType().Name), GameConsole.MessageType.Error); // Debug
                return 0;
            }

            // Queue all layers for removal
            Action rem = new Action(delegate { });
            int length = _renderables.Count;
            for (int i = length - 1; i >= 0; i--)
            {
                // Keep entity and index
                int index = i;

                // Create action that removes selected layer
                rem += delegate
                {
                    // Remove layer from container
                    _renderables.RemoveAt(index);
                };
            }

            // Remove all entities
            rem();
            GameConsole.WriteLine(string.Format("{0}: Removed all GraphicsLayers (Count {1})", GetType().Name, length)); // Debug

            // Return amount of entities removed
            return length;
        }

        //
        public bool Contains(IRenderable rend)
        {
            // Compare paramter layer reference to every layer in the container
            int length = _renderables.Count;
            for (int i = 0; i < length; i++)
            {
                if (ReferenceEquals(_renderables[i], rend))
                    return true; // Success
            }

            // Failed to find layer in the collection
            return false;
        }
    }
}
