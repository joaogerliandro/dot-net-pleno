using StallosDotnetPleno.Domain.Entities;

namespace StallosDotnetPleno.Application.Interfaces
{
    public interface IRosterApiService
    {
        Task<List<PublicList>> ConsultPersonPublicList(Person person);
    }
}
