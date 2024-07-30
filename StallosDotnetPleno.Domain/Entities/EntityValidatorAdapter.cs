using FluentValidation;
using FluentValidation.Results;
using StallosDotnetPleno.Domain.Entities;
using StallosDotnetPleno.Domain.Notifications;

namespace StallosDotnetPleno.Domain.Validators
{
    public class EntityValidatorAdapter<T> where T : BaseEntity
    {
        private readonly IValidator<T> _validator;

        public EntityValidatorAdapter(IValidator<T> validator)
        {
            _validator = validator;
        }

        public void Validate(T entity, Notifier notifier)
        {
            ValidationResult result = _validator.Validate(entity);

            foreach (var error in result.Errors)
            {
                notifier.AddNotification(error.PropertyName, error.ErrorMessage);
            }
        }
    }
}