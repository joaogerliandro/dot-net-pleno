using StallosDotnetPleno.Domain.Entities;
using System.Linq.Expressions;

namespace StallosDotnetPleno.Application.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetAllAsync();
        Task<Person> GetByIdAsync(long id);
        Task AddAsync(Person person);
        Task UpdateAsync(Person person);
        Task DeleteAsync(long id);
        Task<IEnumerable<Person>> FindAsync(Expression<Func<Person, bool>> predicate);
    }
}
