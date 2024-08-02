using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StallosDotnetPleno.Application.Interfaces;

namespace StallosDotnetPleno.Application.Services
{
    public class BackgroundTaskService : BackgroundService
    {
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<BackgroundTaskService> _logger;

        public BackgroundTaskService(IBackgroundTaskQueue taskQueue, ILogger<BackgroundTaskService> logger, IServiceProvider serviceProvider)
        {
            _taskQueue = taskQueue;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Background Task Service is running.");

            await ExecuteBackgroundTasks(stoppingToken);
        }

        private async Task ExecuteBackgroundTasks(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await _taskQueue.DequeueAsync(stoppingToken);

                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var scopedProcessingService = scope.ServiceProvider.GetRequiredService<IBackgroundProcessingService>();
                        await scopedProcessingService.ProcessWorkItemAsync(workItem, stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred executing {WorkItem}.", nameof(workItem));
                }
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Background Task Service is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}