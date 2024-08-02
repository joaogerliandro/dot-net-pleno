using Microsoft.Extensions.DependencyInjection;
using StallosDotnetPleno.Application.Interfaces;
using StallosDotnetPleno.Domain.Entities;
using StallosDotnetPleno.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace StallosDotnetPleno.Application.Services
{
    public class BackgroundProcessingService : IBackgroundProcessingService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IRosterApiService _rosterApiService;

        public BackgroundProcessingService(IServiceScopeFactory serviceScopeFactory, IRosterApiService rosterApiService)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _rosterApiService = rosterApiService;
        }

        public async Task ProcessWorkItemAsync(Func<CancellationToken, Task> workItem, CancellationToken cancellationToken)
        {
            await workItem(cancellationToken);
        }

        public async Task ConsultPersonPublicListAsync(Person person)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var personRepository = scope.ServiceProvider.GetRequiredService<IPersonRepository>();

                var personPublicList = await _rosterApiService.ConsultPersonPublicList(person);

                if (personPublicList != null && personPublicList.Count > 0)
                {
                    await personRepository.UpdatePersonListAsync(person.Id, personPublicList);
                }
            }
        }
    }
}