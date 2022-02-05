namespace MediatRFluentDemo.Features.Milestones.Commands.DeleteMilestone
{
    using MediatR;
    using MediatRFluentDemo.Abstractions;
    using System;

    public class DeleteMilestoneCommand : IRequest<Guid>, ICacheableQuery
    {
        public Guid Id { get; set; }

        public bool BypassCache => true;

        public string CacheKey => null;

        public TimeSpan? Expiration => TimeSpan.Zero;
    }
}
