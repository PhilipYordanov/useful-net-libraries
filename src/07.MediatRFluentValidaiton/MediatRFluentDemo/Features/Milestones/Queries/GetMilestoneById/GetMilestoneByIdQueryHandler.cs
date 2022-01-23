namespace MediatRFluentDemo.Features.Milestones.Queries.GetMilestoneById
{
    using MediatR;
    using MediatRFluentDemo.Context;
    using MediatRFluentDemo.Features.Milestones.Queries.GetAllMilestones;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetMilestoneByIdQueryHandler : IRequestHandler<GetMilestoneByIdQuery, Milestone>
    {
        //private readonly IApplicationContext _context;
        IMediator mediator;

        //public GetMilestoneByIdQueryHandler(IApplicationContext context)
        //{
        //    this._context = context;
        //}
        public GetMilestoneByIdQueryHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Milestone> Handle(GetMilestoneByIdQuery request, CancellationToken cancellationToken)
        {
            //Milestone result = await this._context.Milestones
            //    .FirstOrDefaultAsync(x=>x.Id == request.Id);

            List<Milestone> milestones = await mediator.Send(new GetAllMilestonesQuery() { BypassCache = false });
            Milestone result = milestones
                .FirstOrDefault(x => x.Id == request.Id);

            if (result == null)
            {
                return null;
            }

            return result;
        }
    }
}
