using StallosDotnetPleno.Application.Interfaces;
using StallosDotnetPleno.Domain.Entities;
using StallosDotnetPleno.Infrastructure.Interfaces;
using System.Linq.Expressions;

namespace StallosDotnetPleno.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IRepository<Person> _personRepository;

        public PersonService(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _personRepository.GetAllAsync();
        }

        public async Task<Person> GetByIdAsync(long id)
        {
            return await _personRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Person person)
        {
            await _personRepository.AddAsync(person);
        }

        public async Task UpdateAsync(Person person)
        {
            await _personRepository.UpdateAsync(person);
        }

        public async Task DeleteAsync(long id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person != null)
            {
                await _personRepository.DeleteAsync(person);
            }
        }

        public async Task<IEnumerable<Person>> FindAsync(Expression<Func<Person, bool>> predicate)
        {
            return await _personRepository.FindAsync(predicate);
        }
    }
}
