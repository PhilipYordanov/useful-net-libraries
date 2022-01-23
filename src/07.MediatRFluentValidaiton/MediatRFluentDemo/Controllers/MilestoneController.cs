
namespace MediatRFluentDemo.Controllers
{
    using Features.Milestones.Commands.CreateMilestone;
    using Features.Milestones.Commands.DeleteMilestone;
    using Features.Milestones.Commands.UpdateMilestone;
    using Features.Milestones.Queries.GetAllMilestones;
    using Features.Milestones.Queries.GetMilestoneById;
    using MediatR;
    using MediatRFluentDemo.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    [Route("api/milestones")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
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

        /// <summary>
        /// Get an milestone by id
        /// </summary>
        /// <param name="id">The id of the milestone we want to get</param>
        /// <returns>An ActioResult of type Milestone</returns>
        /// <remarks>
        /// Sample request ( this request gets the milestone by **id '92238e49-2b99-4318-256c-08d9d82f6ed5'** if exists  
        /// ```
        /// Get /milestones/id  
        /// [  
        ///     {  
        ///         "id": "92238e49-2b99-4318-256c-08d9d82f6ed5"  
        ///     }  
        /// ]
        /// ```
        /// </remarks>
        /// <response code="200">Returns the requested Milestone</response>
        [HttpGet("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Milestone)] in case of IActionResult
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))] // predefined response types for GET. applied on method level overrides the one applied on controller level
        public async Task<ActionResult<Milestone>> GetById(Guid id)
        //public async Task<IActionResult> GetById(Guid id)
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
