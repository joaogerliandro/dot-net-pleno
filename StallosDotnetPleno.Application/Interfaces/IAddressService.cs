using StallosDotnetPleno.Domain.Entities;
using System.Linq.Expressions;

namespace StallosDotnetPleno.Application.Interfaces
{
    public interface IAddressService
    {
        Task<IEnumerable<Address>> GetAllAsync();
        Task<Address> GetByIdAsync(long id);
        Task AddAsync(Address address);
        Task UpdateAsync(Address address);
        Task DeleteAsync(long id);
        Task<IEnumerable<Address>> FindAsync(Expression<Func<Address, bool>> predicate);
    }
}
