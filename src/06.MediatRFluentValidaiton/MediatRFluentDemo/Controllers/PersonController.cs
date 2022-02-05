using MediatR;
using MediatRFluentDemo.Features.People.Commands.CreatePerson;
using Microsoft.AspNetCore.Mvc;

namespace MediatRFluentDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PersonController(IMediator mediator) => _mediator = mediator;


        [HttpPost]
        public async Task<IActionResult> Create(CreatePersonCommand command) => Ok(await this._mediator.Send(command));

        //[HttpPost]
        //public Task<IActionResult> Create(Person command)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
