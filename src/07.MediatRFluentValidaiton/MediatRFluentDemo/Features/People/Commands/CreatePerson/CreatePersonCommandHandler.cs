namespace MediatRFluentDemo.Features.People.Commands.CreatePerson
{
    using MediatR;
    using RestrictedModels;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Person>
    {
        public Task<Person> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            Person person = new Person
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth
            };

            // should not goes here if validation fails;
            throw new System.NotImplementedException();
        }
    }
}
