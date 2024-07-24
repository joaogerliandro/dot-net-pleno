using StallosDotnetPleno.Domain.Interfaces;
using StallosDotnetPleno.Domain.Notifications;
using System.ComponentModel.DataAnnotations;

namespace StallosDotnetPleno.Domain.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; protected set; }

        protected Notifier Notifier { get; } = new Notifier();

        protected IValidator<BaseEntity> Validator { get; set; }

        public bool IsValid { get; private set; }

        public IReadOnlyCollection<Notification> GetNotifications() => Notifier.GetNotifications();

        public bool HasNotifications() => Notifier.HasNotifications();

        public void SetValidator<T>(IValidator<T> validator) where T : BaseEntity
        {
            Validator = new EntityValidatorAdapter<T>(validator) as IValidator<BaseEntity>;
        }

        public void Validate()
        {
            Validator?.Validate(this, Notifier);
            IsValid = !(HasNotifications());
        }
    }
}
