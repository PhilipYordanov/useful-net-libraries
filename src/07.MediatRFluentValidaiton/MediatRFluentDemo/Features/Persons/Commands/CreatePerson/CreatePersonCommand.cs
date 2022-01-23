namespace MediatRFluentDemo.Features.Persons.Commands.CreatePerson
{
    using MediatR;
    using RestrictedModels;
    using System;

    public class CreatePersonCommand : IRequest<Person>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

    }
}
