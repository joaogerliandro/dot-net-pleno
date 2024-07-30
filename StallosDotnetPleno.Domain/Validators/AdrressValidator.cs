using FluentValidation;
using StallosDotnetPleno.Domain.Entities;

namespace StallosDotnetPleno.Domain.Validators
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(address => address.ZipCode)
                .NotEmpty().WithMessage("ZipCode cannot be empty.")
                .NotNull().WithMessage("ZipCode cannot be null.")
                .MaximumLength(10).WithMessage("ZipCode cannot exceed 10 characters.");

            RuleFor(address => address.Street)
                .NotEmpty().WithMessage("Street cannot be empty.")
                .NotNull().WithMessage("Street cannot be null.")
                .MaximumLength(255).WithMessage("Street cannot exceed 255 characters.");

            RuleFor(address => address.Number)
                .NotEmpty().WithMessage("Number cannot be empty.")
                .NotNull().WithMessage("Number cannot be null.")
                .MaximumLength(10).WithMessage("Number cannot exceed 10 characters.");

            RuleFor(address => address.District)
                .NotEmpty().WithMessage("District cannot be empty.")
                .NotNull().WithMessage("District cannot be null.")
                .MaximumLength(255).WithMessage("District cannot exceed 255 characters.");

            RuleFor(address => address.City)
                .NotEmpty().WithMessage("City cannot be empty.")
                .NotNull().WithMessage("City cannot be null.")
                .MaximumLength(255).WithMessage("City cannot exceed 255 characters.");

            RuleFor(address => address.StateCode)
                .NotEmpty().WithMessage("StateCode cannot be empty.")
                .NotNull().WithMessage("StateCode cannot be null.")
                .MaximumLength(2).WithMessage("StateCode cannot exceed 2 characters.");
        }
    }
}