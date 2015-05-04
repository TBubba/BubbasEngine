using System;
using System.IO;

namespace BubbasEngine.Engine.Content
{
    public delegate object ProcessContent(Stream fileStream);

    /// <summary>
    /// References content managed by the content manager
    /// </summary>
    public class ContentContainer
    {
        /// <summary>
        /// The contained content
        /// </summary>
        public object Content
        { get; internal set; }

        /// <summary>
        /// The path to where the content was loaded from (including contents file name)
        /// (This might not be an actual file, since the content could have been loaded from memory or the file could have been moved/removed)
        /// </summary>
        public string Path
        { get; internal set; }

        /// <summary>
        /// SHA1 hash of the content
        /// </summary>
        public string Hash
        { get; internal set; }

        /// <summary>
        /// If the content was loaded asynchronously
        /// </summary>
        internal bool LoadedAsync // Make public ?
        { get; set; }

        /// <summary>
        /// If the content was loaded
        /// (if false, content is still loading)
        /// </summary>
        public bool LoadingComplete
        { get; internal set; }

        /// <summary>
        /// The function used for processing the content
        /// </summary>
        internal ProcessContent Process // Make public ?
        { get; set; }

        // Constructor(s)
        internal ContentContainer()
        {
        }
        internal ContentContainer(string path)
        {
            Path = path;
        }

        /// <summary>
        /// Gets the content as type T
        /// (easy way of getting content as its actual type)
        /// </summary>
        /// <typeparam name="T">Type to get content as</typeparam>
        /// <returns>Content</returns>
        public T GetContent<T>()
        {
            return (T)Content;
        }

        /// <summary>
        /// Checks whether or not the content is of a value type (class / non-struct)
        /// </summary>
        /// <returns>If the content is of a value type</returns>
        public bool IsValueType()
        {
            return (Content is ValueType);
        }
    }
}
