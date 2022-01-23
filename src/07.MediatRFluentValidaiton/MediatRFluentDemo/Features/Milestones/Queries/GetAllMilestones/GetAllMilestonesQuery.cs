namespace MediatRFluentDemo.Features.Milestones.Queries.GetAllMilestones
{
    using MediatR;
    using MediatRFluentDemo.Abstractions;
    using Models;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// IRequest where T is the return type
    /// </summary>
    public class GetAllMilestonesQuery : IRequest<List<Milestone>>, ICacheableQuery
    {
        public bool BypassCache { get; set; }

        public string CacheKey => "GetAllMilestones";

        public TimeSpan? Expiration { get; set; }
    }
}
