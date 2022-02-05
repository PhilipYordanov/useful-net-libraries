namespace MediatRFluentDemo.Features.Milestones.Queries.GetMilestoneById
{
    using MediatR;
    using MediatRFluentDemo.Abstractions;
    using Models;
    using System;

    public class GetMilestoneByIdQuery : IRequest<Milestone> , ICacheableQuery
    {
        public Guid Id { get; set; }

        public bool BypassCache { get; set; }

        public string CacheKey => $"GetMilestoneById-{Id}";

        public TimeSpan? Expiration { get; set; }
}
}
