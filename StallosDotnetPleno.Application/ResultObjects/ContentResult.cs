using StallosDotnetPleno.Domain.Notifications;

namespace StallosDotnetPleno.Application.ResultObjects
{
    public class ContentResult : BaseResult
    {
        public object Content { get; set; }

        public object Notifications { get; set; }
    }
}
