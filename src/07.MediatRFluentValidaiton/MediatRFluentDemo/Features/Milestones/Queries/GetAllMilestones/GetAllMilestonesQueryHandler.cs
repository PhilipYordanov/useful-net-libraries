namespace MediatRFluentDemo.Features.Milestones.Queries.GetAllMilestones
{
    using Context;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// IRequestHandler suggests that we are going to handle GetAllMilestonesQuery, and return list of milestones
    /// </summary>
    public class GetAllMilestonesQueryHandler : IRequestHandler<GetAllMilestonesQuery, List<Milestone>>
    {
        private readonly IApplicationContext _context;

        public GetAllMilestonesQueryHandler(IApplicationContext context)
        {
            this._context = context;
        }

        public async Task<List<Milestone>> Handle(GetAllMilestonesQuery request, CancellationToken cancellationToken)
        {
            var milestones = await _context.Milestones.ToListAsync();
            if (milestones == null)
            {
                return null;
            }

            return milestones;
        }
    }
}
