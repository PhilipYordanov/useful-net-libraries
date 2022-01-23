namespace MediatRFluentDemo.Features.Milestones.Commands.DeleteMilestone
{
    using MediatR;
    using Context;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeleteMilestoneCommandHandler : IRequestHandler<DeleteMilestoneCommand, Guid>
    {
        private readonly IApplicationContext _context;

        public DeleteMilestoneCommandHandler(IApplicationContext context)
        {
            this._context = context;
        }

        public async Task<Guid> Handle(DeleteMilestoneCommand request, CancellationToken cancellationToken)
        {
            var milestone = await _context.Milestones.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
            if (milestone == null)
            {
                return default;
            }

            _context.Milestones.Remove(milestone);
            await _context.SaveChangesAsync();

            return milestone.Id;
        }
    }
}
