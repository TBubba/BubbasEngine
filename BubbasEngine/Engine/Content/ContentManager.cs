using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.GameStates;
using SFML.Graphics;
using System.IO;
using System.Threading;
using System.Collections;

namespace BubbasEngine.Engine.Content
{
    public class ContentManager
    {
        // Static Content Processing Functions
        public static object ProcessByteArray(Stream stream)
        {
            int length = (int)stream.Length;
            byte[] buffer = new byte[length];
            int count;
            int sum = 0;

            // Read stream until end
            while ((count = stream.Read(buffer, sum, length - sum)) > 0)
                sum += count;

            // Return byte array
            return buffer;
        }
        public static object ProcessTexture(Stream stream)
        {
            // Create and return texture
            return new Texture(stream);
        }

        // Content 
        private string _basePath; // Path to the content base directory
        private Dictionary<string, ContentContainer> _content; // Content Containers (Content path, Content Container class)
        private Dictionary<string, ProcessContent> _extensions; // Processes 

        private FileSystemWatcher _watcher;

        //
        public Dictionary<string, ProcessContent> Extensions
        { get { return _extensions; } }

        // Constructor(s)
        internal ContentManager(ContentManagerArgs args)
        {
            // Initialize dictionaries
            _content = new Dictionary<string, ContentContainer>();
            _extensions = new Dictionary<string,ProcessContent>(args.Extensions); // Set extensions from args

            // Set content base path (after arguments)
            if (args.RelativePath) // If the path is relative
                _basePath += AppDomain.CurrentDomain.BaseDirectory; // Add ...
            _basePath += args.ContentPath;

            // Initialize file watcher
            _watcher = new FileSystemWatcher(_basePath.Remove(_basePath.Length - 1, 1));
            _watcher.IncludeSubdirectories = true;
            _watcher.EnableRaisingEvents = true;

            _watcher.NotifyFilter = NotifyFilters.LastAccess |
                                    NotifyFilters.LastWrite |
                                    NotifyFilters.FileName |
                                    NotifyFilters.DirectoryName;

            _watcher.Changed += new FileSystemEventHandler(OnChanged);
            _watcher.Created += new FileSystemEventHandler(OnChanged);
            _watcher.Deleted += new FileSystemEventHandler(OnChanged);
            _watcher.Renamed += new RenamedEventHandler(OnRenamed);
        }

        // Watcher events
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            // Get file path (relative to base path)
            string path = e.FullPath.Remove(0, _basePath.Length);

            // Abort if file is not loaded
            if (!_content.ContainsKey(path)) // Check if file is not used for any content
                return;

            // Reload file
            ContentContainer cc = _content[path]; // Get content container
            if (cc.LoadedAsync) // Check if file should be loaded asynchronously or not
                LoadFromFileAsync(cc);
            else
                LoadFromFile(cc);
        }
        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            // Not sure what to do (since content is identified by their name)
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ContentContainer LoadContent(string path)
        {
            return LoadContent(path, AssumeProcess(path));
        }
        public ContentContainer LoadContent(string path, ProcessContent process)
        {
            ContentContainer cc;
            if (!_content.TryGetValue(path, out cc)) // Check if content is not already loaded
            {
                // Create content container for content
                cc = new ContentContainer(path);
                cc.LoadedAsync = false;
                cc.Process = process;

                // Load content from file
                LoadFromFile(cc);
            }

            // Return content container
            return cc;
        }

        public ContentContainer LoadContentAsync(string path)
        {
            return LoadContentAsync(path, AssumeProcess(path));
        }
        public ContentContainer LoadContentAsync(string path, ProcessContent process)
        {
            ContentContainer cc;
            if (!_content.TryGetValue(path, out cc)) // Check if content is not already loaded
            {
                // Create container file
                cc = new ContentContainer(path);
                cc.LoadedAsync = true;
                cc.LoadingComplete = false;
                cc.Process = process;

                // Load content from file
                LoadFromFileAsync(cc);
            }

            // Return content container
            return cc;
        }

        //
        private void LoadFromFile(ContentContainer cc)
        {
            // Load content
            object content = null;
            using (Stream stream = new FileStream(_basePath + cc.Path, FileMode.Open, FileAccess.Read)) // Open stream
            {
                // Process file content
                content = cc.Process(stream);

                // Close stream
                stream.Close();
            }
            
            // Add content to container
            cc.Content = content;
            cc.Hash = HashObject(content);
            cc.LoadingComplete = true;
        }
        private void LoadFromFileAsync(ContentContainer cc)
        {
            // Try loading content on another thread
            bool s = ThreadPool.QueueUserWorkItem((o) =>
                {
                    // Load content from file
                    object content = null;
                    using (Stream stream = new FileStream(_basePath + cc.Path, FileMode.Open, FileAccess.Read))
                    {
                        // Process file content
                        content = cc.Process(stream);

                        // Close stream
                        stream.Close();
                    }
                    
                    // Add content to container
                    cc.Content = content;
                    cc.Hash = HashObject(cc.Content);
                    cc.LoadingComplete = true;
                });

            // Something went wrong when trying to queue the loading method
            if (!s)
                throw new Exception("aids");
                
        }

        private ProcessContent AssumeProcess(string path)
        {
            // Get filename extension
            string[] splits = path.Split('.');
            string extension = splits[splits.Length - 1];

            // Find process for extension
            ProcessContent process;
            if (_extensions.TryGetValue(extension, out process))
            {
                // Return process
                return process;
            }

            // Return default process (if there is no process for the extension)
            return ProcessByteArray;
        }

        private string HashObject(object obj)
        {
            return "";
        }

        // Contains
        internal bool ContainsPath(string path)
        {
            return _content.ContainsKey(path);
        }
        internal bool ContainsContent(ContentContainer content)
        {
            return _content.ContainsValue(content);
        }
    }
}
