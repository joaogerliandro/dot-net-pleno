using FluentValidation;
using StallosDotnetPleno.Domain.Entities;
using StallosDotnetPleno.Domain.Enums;

namespace StallosDotnetPleno.Domain.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(person => person.Name)
                .NotEmpty().WithMessage("Name cannot be empty.")
                .NotNull().WithMessage("Name cannot be null.")
                .MaximumLength(255).WithMessage("Name cannot exceed 255 characters.");

            RuleFor(person => person.Document)
                .NotEmpty().WithMessage("Document cannot be empty.")
                .NotNull().WithMessage("Document cannot be null.")
                .MaximumLength(14).WithMessage("Document cannot exceed 14 characters.")
                .MinimumLength(11).WithMessage("Document must have at least 11 characters.");

            RuleFor(person => person.Type)
                .NotEmpty().WithMessage("Type cannot be empty.")
                .NotNull().WithMessage("Type cannot be null.")
                .Must(BeAValidPersonType).WithMessage(person => String.Format("Type '{0}' is not valid.", person.Type));

            RuleFor(person => person.Addresses)
                .NotEmpty().WithMessage("Person must have at least one address.")
                .NotNull().WithMessage("Address list cannot be null.");

            RuleForEach(person => person.Addresses)
    .           SetValidator(new AddressValidator());
        }

        private bool BeAValidPersonType(string personType)
        {
            return Enum.TryParse(typeof(PersonType), personType, true, out _);
        }
    }
}