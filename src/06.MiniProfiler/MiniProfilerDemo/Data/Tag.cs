using System.Collections.Generic;

namespace MiniProfilerDemo.Data
{
    public class Tag
    {
        public string TagId { get; set; }

        public string Name { get; set; }

        public List<PostTag> PostTags { get; set; }
    }
}
