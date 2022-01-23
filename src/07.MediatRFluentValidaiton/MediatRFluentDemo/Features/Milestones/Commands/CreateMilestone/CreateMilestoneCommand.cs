namespace MediatRFluentDemo.Features.Milestones.Commands.CreateMilestone
{
    using MediatR;
    using Models;
    using System;

    public class CreateMilestoneCommand : IRequest<Milestone>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime AchivedOn { get; set; }

        public string AdditionalDetails { get; set; }
    }
}
