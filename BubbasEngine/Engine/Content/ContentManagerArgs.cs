using System.Collections.Generic;

namespace BubbasEngine.Engine.Content
{
    public class ContentManagerArgs
    {
        // Content Path
        public string ContentPath;
        public bool RelativePath;

        public bool SafeContentLoading;

        public IDictionary<string, ProcessContent> Extensions;

        // Constructor(s)
        public ContentManagerArgs()
        {
            ContentPath = @"content\";
            RelativePath = true;
            SafeContentLoading = false;
            Extensions = new Dictionary<string, ProcessContent>();

            // Standard extensions
            Extensions.Add("png", ContentManager.ProcessTexture);
            Extensions.Add("jpg", ContentManager.ProcessTexture);
            Extensions.Add("jpeg", ContentManager.ProcessTexture);
            Extensions.Add("bmp", ContentManager.ProcessTexture);
        }
        public ContentManagerArgs(ContentManagerArgs args)
        {
            ContentPath = args.ContentPath;
            RelativePath = args.RelativePath;
            SafeContentLoading = args.SafeContentLoading;
            Extensions = new Dictionary<string, ProcessContent>(args.Extensions);
        }
    }
}
