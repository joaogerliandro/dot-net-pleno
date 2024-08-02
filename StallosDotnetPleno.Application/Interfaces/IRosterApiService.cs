using StallosDotnetPleno.Domain.Entities;

namespace StallosDotnetPleno.Application.Interfaces
{
    public interface IRosterApiService
    {
        Task<ICollection<PublicList>> ConsultPersonPublicList(Person person);
    }
}
