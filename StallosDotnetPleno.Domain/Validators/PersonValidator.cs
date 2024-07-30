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
                .Must((person, document) => IsValidDocumentForType(person.Type, document))
                .WithMessage(person => GetDocumentValidationErrorMessage(person.Type, person.Document));

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

        private bool IsValidDocumentForType(string type, string document)
        {
            if (Enum.TryParse(typeof(PersonType), type, out var personType))
            {
                if ((PersonType)personType == PersonType.PF)
                {
                    return DocumentValidator.IsValidCpf(document);
                }
                else if ((PersonType)personType == PersonType.PJ)
                {
                    return DocumentValidator.IsValidCnpj(document);
                }
            }

            return false;
        }

        private string GetDocumentValidationErrorMessage(string type, string document)
        {
            bool isCpf = DocumentValidator.IsValidCpf(document);
            bool isCnpj = DocumentValidator.IsValidCnpj(document);

            if (Enum.TryParse(typeof(PersonType), type, out var personType))
            {
                if ((PersonType)personType == PersonType.PF)
                {
                    return isCpf ? "Type should be PJ, but Document is valid for Type PF." : "Document is invalid for Type PF.";
                }
                else if ((PersonType)personType == PersonType.PJ)
                {
                    return isCnpj ? "Type should be PF, but Document is valid for Type PJ." : "Document is invalid for Type PJ.";
                }
            }

            return "Document and Type are both invalid.";
        }
    }
}