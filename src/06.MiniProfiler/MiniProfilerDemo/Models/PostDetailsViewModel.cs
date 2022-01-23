using System.Collections.Generic;

namespace MiniProfilerDemo.Models
{
    public class PostDetailsViewModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public List<TagViewModel> Tags { get; set; }
    }
}
