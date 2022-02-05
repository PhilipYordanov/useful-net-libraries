namespace MediatRFluentDemo.Features.Milestones.Commands.UpdateMilestone
{
    using MediatR;
    using MediatRFluentDemo.Abstractions;
    using System;

    public class UpdateMilestoneCommand : IRequest
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime AchivedOn { get; set; }

        public string AdditionalDetails { get; set; }
    }
}
