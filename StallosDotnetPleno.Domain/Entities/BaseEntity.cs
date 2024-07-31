using FluentValidation;
using StallosDotnetPleno.Domain.Notifications;
using StallosDotnetPleno.Domain.Validators;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StallosDotnetPleno.Domain.Entities
{
    public abstract class BaseEntity
    {
        [JsonIgnore]
        public long Id { get; protected set; }

        [NotMapped]
        [JsonIgnore]
        protected Notifier Notifier { get; } = new Notifier();
        
        [NotMapped]
        [JsonIgnore]
        protected IValidator<BaseEntity> Validator { get; set; }

        [NotMapped]
        [JsonIgnore]
        public bool IsValid { get; private set; }

        public IReadOnlyCollection<Notification> GetNotifications() => Notifier.GetNotifications();

        public bool HasNotifications() => Notifier.HasNotifications();

        public void SetValidator<T>(IValidator<T> validator) where T : BaseEntity
        {
            Validator = new EntityValidatorAdapter<T>(validator) as IValidator<BaseEntity>;
        }

        public void Validate()
        {
            var validationResult = Validator.Validate(this);

            foreach (var error in validationResult.Errors)
            {
                Notifier.AddNotification(error.PropertyName, error.ErrorMessage);
            }

            IsValid = !Notifier.HasNotifications();
        }
    }
}
