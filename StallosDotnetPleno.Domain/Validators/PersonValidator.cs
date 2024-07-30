using FluentValidation;
using StallosDotnetPleno.Domain.Entities;

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

            RuleFor(person => person.Addresses)
                .NotEmpty().WithMessage("Person must have at least one address.")
                .NotNull().WithMessage("Address list cannot be null.");
        }
    }
}