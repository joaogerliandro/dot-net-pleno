using StallosDotnetPleno.Domain.Entities;

namespace StallosDotnetPleno.Application.Interfaces
{
    public interface IBackgroundProcessingService
    {
        Task ProcessWorkItemAsync(Func<CancellationToken, Task> workItem, CancellationToken stoppingToken);
        Task ConsultPersonPublicListAsync(Person person);
    }
}
