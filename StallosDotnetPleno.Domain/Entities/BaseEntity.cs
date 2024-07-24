using StallosDotnetPleno.Domain.Notifications;

namespace StallosDotnetPleno.Domain.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }

        protected Notifier Notifier { get; } = new Notifier();

        public IReadOnlyCollection<Notification> GetNotifications() => Notifier.GetNotifications();

        public bool HasNotifications() => Notifier.HasNotifications();
    }
}
