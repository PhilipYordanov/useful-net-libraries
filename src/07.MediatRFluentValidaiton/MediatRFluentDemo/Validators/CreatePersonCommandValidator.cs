namespace MediatRFluentDemo.Validators
{
    using Features.People.Commands.CreatePerson;
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using RestrictedModels;
    using System;
    using System.Linq;

    // AbstractValidator - base class provided from fluent validation library so we can create own validators
    // public class CreatePersonCommandValidator : AbstractValidator<Person>
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator(IStringLocalizer<CreatePersonCommandValidator> localizer)
        {
            RuleFor(e => e.FirstName)
                .Cascade(CascadeMode.Stop) // without this line we will retrieve two error messages;
                .NotEmpty() 
                .WithMessage(e => string.Format(localizer["FirstNameMessage"], nameof(e.FirstName)))
                .Length(1, 30)
                .Must(IsValidName);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage(x => localizer["LastNameMessage"]);

            RuleFor(x => x.DateOfBirth)
                .Must(BeAValidAge)
                .WithMessage("Invalid {PropertyName}"); // {PropertyName} <-- FluentValidation reserved word
        }

        private bool IsValidName(string name)
        {
            return name.All(char.IsLetter);
        }

        private bool BeAValidAge(DateTime date)
        {
            if (date >= DateTime.Now)
            {
                return false;
            }

            return true;
        }
    }
}
