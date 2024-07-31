using StallosDotnetPleno.Domain.Entities;

namespace StallosDotnetPleno.Infrastructure.Interfaces
{
    public interface IPersonRepository
    {
        Task AddAsync(Person entity);
        Task<Person> GetByIdAsync(long id);
        Task<IEnumerable<Person>> GetAllAsync();
        Task<Person> GetByDocumentAsync(string document);
        Task UpdateAsync(Person person);
        Task DeleteAsync(Person person);
    }
}
