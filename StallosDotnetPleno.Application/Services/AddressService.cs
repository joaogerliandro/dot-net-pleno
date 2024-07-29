using StallosDotnetPleno.Application.Interfaces;
using StallosDotnetPleno.Domain.Entities;
using StallosDotnetPleno.Domain.Interfaces;
using StallosDotnetPleno.Infrastructure.Interfaces;
using System.Linq.Expressions;

namespace StallosDotnetPleno.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IRepository<Address> _repository;
        private readonly IValidator<Address> _validator;

        public AddressService(IRepository<Address> repository, IValidator<Address> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Address> GetByIdAsync(long id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(Address address)
        {
            address.SetValidator(_validator);
            address.Validate();

            if(!address.IsValid)
            {
                // Notify
            }

            await _repository.AddAsync(address);
        }

        public async Task UpdateAsync(Address address)
        {
            address.SetValidator(_validator);
            address.Validate();

            if (!address.IsValid)
            {
                // Notify
            }

            await _repository.UpdateAsync(address);
        }

        public async Task DeleteAsync(long id)
        {
            var address = await _repository.GetByIdAsync(id);

            if (address == null)
            {
                // Notify
            }

            await _repository.DeleteAsync(address);
        }

        public async Task<IEnumerable<Address>> FindAsync(Expression<Func<Address, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }
    }
}
