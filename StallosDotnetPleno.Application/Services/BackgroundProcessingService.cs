using StallosDotnetPleno.Application.Interfaces;
using StallosDotnetPleno.Domain.Entities;
using StallosDotnetPleno.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace StallosDotnetPleno.Application.Services
{
    public class BackgroundProcessingService : IBackgroundProcessingService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IRosterApiService _rosterApiService;

        public BackgroundProcessingService(IPersonRepository personRepository, IRosterApiService rosterApiService)
        {
            _personRepository = personRepository;
            _rosterApiService = rosterApiService;
        }

        public async Task ProcessWorkItemAsync(Func<CancellationToken, Task> workItem, CancellationToken cancellationToken)
        {
            await workItem(cancellationToken);
        }

        public async Task ConsultPersonPublicListAsync(Person person)
        {
            var personPublicList = await _rosterApiService.ConsultPersonPublicList(person);

            if (personPublicList != null && personPublicList.Count > 0)
            {
                await _personRepository.UpdatePersonListAsync(person.Id, personPublicList);
            }
        }
    }
}