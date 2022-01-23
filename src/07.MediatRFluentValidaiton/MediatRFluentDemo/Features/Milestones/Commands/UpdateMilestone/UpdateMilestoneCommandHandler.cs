namespace MediatRFluentDemo.Features.Milestones.Commands.UpdateMilestone
{
    using MediatR;
    using Context;
    using Models;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class UpdateMilestoneCommandHandler : IRequestHandler<UpdateMilestoneCommand>
    {
        private readonly IApplicationContext _context;

        public UpdateMilestoneCommandHandler(IApplicationContext context)
        {
            this._context = context;
        }

        public async Task<Unit> Handle(UpdateMilestoneCommand request, CancellationToken cancellationToken)
        {
            Milestone milestone = _context.Milestones
                .Where(a => a.Id == request.Id)
                .FirstOrDefault();

            if (milestone == null)
            {
                return default;
            }

            milestone.Title = request.Title;
            milestone.Content = request.Content;
            milestone.AdditionalDetails = request.AdditionalDetails;
            milestone.AchivedOn = request.AchivedOn;

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
