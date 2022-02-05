using MediatRFluentDemo.Models.Common;

namespace MediatRFluentDemo.Models
{
    public class Milestone : AuditableEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime AchivedOn { get; set; }

        public string AdditionalDetails { get; set; }
    }
}
