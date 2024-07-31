using StallosDotnetPleno.Domain.Entities;

namespace StallosDotnetPleno.Infrastructure.Interfaces
{
    public interface IAddressRepository
    {
        Task AddAsync(Address address);
        Task<Address> GetByIdAsync(long id);
        Task<IEnumerable<Address>> GetAllAsync();
        Task UpdateAsync(Address address);
        Task DeleteAsync(Address address);
    }
}
