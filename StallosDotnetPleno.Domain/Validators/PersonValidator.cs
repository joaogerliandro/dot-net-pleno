using StallosDotnetPleno.Domain.Entities;
using StallosDotnetPleno.Domain.Notifications;
using FluentValidation;

namespace StallosDotnetPleno.Domain.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public void Validate(Person person, Notifier notifier)
        {
            RuleFor(person => person.Name)
                .NotEmpty().WithMessage("Name cannot be empty.")
                .NotNull().WithMessage("Name cannot be null.")
                .MaximumLength(255).WithMessage("Name cannot exceed 255 characters.");

            RuleFor(person => person.Document)
                .NotEmpty().WithMessage("Document cannot be empty.")
                .NotNull().WithMessage("Document cannot be null.")
                .MaximumLength(14).WithMessage("Document cannot exceed 14 characters.")
                .MinimumLength(11).WithMessage("Document  must have at least 11 characters.");

            RuleFor(person => person.Addresses)
                .NotEmpty().WithMessage("Person must have at least one address.")
                .NotNull().WithMessage("Address list cannot be null.");
        }
    }
}
