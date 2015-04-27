using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;

namespace BubbasEngine.Engine.Graphics
{
    public class GraphicsLayerContainer
    {
        // Private
        private List<GraphicsLayer> _layers;
        private GraphicsRenderer _renderer;

        // Public
        public int Count
        { get { return _layers.Count; } }

        // Container
        public GraphicsLayer this[int index]
        {
            get { return _layers[index]; }
        }

        // Constructor(s)
        internal GraphicsLayerContainer(GraphicsRenderer renderer)
        {
            // Container
            _layers = new List<GraphicsLayer>();

            // Reference to renderer
            _renderer = renderer;
        }

        // Render
        internal void Render(RenderTarget target)
        {
            // Render all
            int length = _layers.Count;
            for (int i = 0; i < length; i++)
                _layers[i].Render(target);
        }

        // Handle layers
        public GraphicsLayer Create()
        {
            GameConsole.WriteLine(string.Format("{0}: New layer created (obj = null)", GetType().Name)); // Debug

            // Create a new layer
            GraphicsLayer layer = new GraphicsLayer(_renderer.DefaultView);

            // Add layer
            _layers.Add(layer);

            // Return layer
            return layer;
        }

        public bool Remove(GraphicsLayer layer)
        {
            // Abort if parameter is null
            if (layer == null)
            {
                GameConsole.WriteLine(string.Format("{0}: Tried to remove a non-existing GraphicsLayer (layer = null)", GetType().Name), GameConsole.MessageType.Error); // Debug
                return false;
            }

            // Abort if layer is not found
            if (!Contains(layer))
            {
                GameConsole.WriteLine(string.Format("{0}: Tried to remove a GraphicsLayer that is not in the container", GetType().Name), GameConsole.MessageType.Error); // Debug
                return false;
            }

            // Remove layer from container
            _layers.Remove(layer);

            // Success
            return true;
        }
        public bool RemoveAt(int index)
        {
            // Abort if index is out of bounds
            if (index < 0 || index >= _layers.Count)
            {
                throw new Exception("index out of bounds");
                //return false; // in case of removal of the exception thrown above
            }

            // Remove layer from container
            _layers.RemoveAt(index);

            // Success
            return true;
        }
        public int RemoveAll()
        {
            // Abort if the container is empty
            if (_layers.Count == 0)
            {
                GameConsole.WriteLine(string.Format("{0}: Tried to remove all GraphicsLayers while none were contained", GetType().Name), GameConsole.MessageType.Error); // Debug
                return 0;
            }

            // Queue all layers for removal
            Action rem = new Action(delegate { });
            int length = _layers.Count;
            for (int i = length - 1; i >= 0; i--)
            {
                // Keep entity and index
                int index = i;

                // Create action that removes selected layer
                rem += delegate
                {
                    // Remove layer from container
                    _layers.RemoveAt(index);
                };
            }

            // Remove all entities
            rem();
            GameConsole.WriteLine(string.Format("{0}: Removed all GraphicsLayers (Count {1})", GetType().Name, length)); // Debug

            // Return amount of entities removed
            return length;
        }

        //
        public bool Contains(GraphicsLayer layer)
        {
            // Compare paramter layer reference to every layer in the container
            int length = _layers.Count;
            for (int i = 0; i < length; i++)
            {
                if (ReferenceEquals(_layers[i], layer))
                    return true; // Success
            }

            // Failed to find layer in the collection
            return false;
        }
    }
}
