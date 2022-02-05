namespace MediatRFluentDemo.Features.Milestones.Commands.CreateMilestone
{
    using MediatR;
    using MediatRFluentDemo.Context;
    using Models;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateMilestoneCommandHandler : IRequestHandler<CreateMilestoneCommand, Milestone>
    {

        private readonly IApplicationContext _context;

        public CreateMilestoneCommandHandler(IApplicationContext context)
        {
            this._context = context;
        }

        public async Task<Milestone> Handle(CreateMilestoneCommand request, CancellationToken cancellationToken)
        {
            Milestone entity = new Milestone
            {
                Title = request.Title,
                Content = request.Content,
                AdditionalDetails = request.AdditionalDetails,
                AchivedOn = request.AchivedOn
            };

            var result =await this._context.Milestones.AddAsync(entity);
            await this._context.SaveChangesAsync();

            return result.Entity;
        }
    }
}
