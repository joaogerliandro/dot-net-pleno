using StallosDotnetPleno.Domain.Interfaces;
using StallosDotnetPleno.Domain.Notifications;

namespace StallosDotnetPleno.Domain.Entities
{
    public class EntityValidatorAdapter<T> : IValidator<BaseEntity> where T : BaseEntity
    {
        private readonly IValidator<T> _validator;

        public EntityValidatorAdapter(IValidator<T> validator)
        {
            _validator = validator;
        }

        public void Validate(BaseEntity entity, Notifier notifier)
        {
            _validator.Validate((T)entity, notifier);
        }
    }
}
