using FluentValidation;
using FluentValidation.Results;

namespace StallosDotnetPleno.Domain.Entities
{
    public class EntityValidatorAdapter<T> : IValidator<BaseEntity> where T : BaseEntity
    {
        private readonly IValidator<T> _validator;

        public EntityValidatorAdapter(IValidator<T> validator)
        {
            _validator = validator;
        }

        public ValidationResult Validate(BaseEntity instance)
        {
            return _validator.Validate((T)instance);
        }

        public async Task<ValidationResult> ValidateAsync(BaseEntity instance, CancellationToken cancellation = default)
        {
            return await _validator.ValidateAsync((T)instance, cancellation);
        }

        public ValidationResult Validate(IValidationContext context) => throw new NotImplementedException();
        public Task<ValidationResult> ValidateAsync(IValidationContext context, CancellationToken cancellation = default) => throw new NotImplementedException();
        public IValidatorDescriptor CreateDescriptor() => throw new NotImplementedException();
        public bool CanValidateInstancesOfType(Type type) => typeof(T).IsAssignableFrom(type);
    }
}