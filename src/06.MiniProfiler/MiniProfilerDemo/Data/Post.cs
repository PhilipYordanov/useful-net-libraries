using System.Collections.Generic;

namespace MiniProfilerDemo.Data
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public List<PostTag> PostTags { get; set; }
    }
}
