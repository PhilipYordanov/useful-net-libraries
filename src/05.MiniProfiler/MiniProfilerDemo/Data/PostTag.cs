using System;

namespace MiniProfilerDemo.Data
{
    public class PostTag
    {
        public DateTime PublicationDate { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }

        public string TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
