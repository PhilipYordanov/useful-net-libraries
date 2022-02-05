
namespace MediatRFluentDemo.Controllers
{
    using Features.Milestones.Commands.CreateMilestone;
    using Features.Milestones.Commands.DeleteMilestone;
    using Features.Milestones.Commands.UpdateMilestone;
    using Features.Milestones.Queries.GetAllMilestones;
    using Features.Milestones.Queries.GetMilestoneById;
    using MediatR;
    using MediatRFluentDemo.Models;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    [Route("api/milestones")]
    [ApiController]
    public class MilestoneController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MilestoneController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create(CreateMilestoneCommand command)
            => Ok(await this._mediator.Send(command));

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await this._mediator.Send(new GetAllMilestonesQuery()
            {
                BypassCache = false
            }));

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Milestone>> GetById(Guid id)
        {
            Milestone result = await this._mediator.Send(new GetMilestoneByIdQuery()
            {
                Id = id,
                BypassCache = false,
                Expiration = new TimeSpan(4, 0, 0)
            });

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await this._mediator.Send(new DeleteMilestoneCommand()
            {
                Id = id
            }));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateMilestoneCommand command)
        {
            await this._mediator.Send(command);
            return NoContent();
        }
    }
}
