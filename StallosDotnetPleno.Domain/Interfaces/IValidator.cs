using StallosDotnetPleno.Domain.Notifications;

namespace StallosDotnetPleno.Domain.Interfaces
{
    public interface IValidator<in T>
    {
        void Validate(T entity, Notifier notifier);
    }
}
