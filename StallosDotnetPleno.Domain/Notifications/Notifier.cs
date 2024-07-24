namespace StallosDotnetPleno.Domain.Notifications
{
    public class Notifier
    {
        private readonly List<Notification> _notifications;

        public Notifier()
        {
            _notifications = new List<Notification>();
        }

        public void AddNotification(string property, string message)
        {
            _notifications.Add(new Notification(property, message));
        }

        public IReadOnlyCollection<Notification> GetNotifications() => _notifications;

        public bool HasNotifications() => _notifications.Any();
    }
}
