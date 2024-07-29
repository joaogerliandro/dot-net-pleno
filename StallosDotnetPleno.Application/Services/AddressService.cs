using StallosDotnetPleno.Application.Interfaces;
using StallosDotnetPleno.Domain.Entities;
using StallosDotnetPleno.Infrastructure.Interfaces;
using System.Linq.Expressions;

namespace StallosDotnetPleno.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IRepository<Address> _addressRepository;

        public AddressService(IRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            return await _addressRepository.GetAllAsync();
        }

        public async Task<Address> GetByIdAsync(long id)
        {
            return await _addressRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Address address)
        {
            await _addressRepository.AddAsync(address);
        }

        public async Task UpdateAsync(Address address)
        {
            await _addressRepository.UpdateAsync(address);
        }

        public async Task DeleteAsync(long id)
        {
            var address = await _addressRepository.GetByIdAsync(id);
            if (address != null)
            {
                await _addressRepository.DeleteAsync(address);
            }
        }

        public async Task<IEnumerable<Address>> FindAsync(Expression<Func<Address, bool>> predicate)
        {
            return await _addressRepository.FindAsync(predicate);
        }
    }
}
