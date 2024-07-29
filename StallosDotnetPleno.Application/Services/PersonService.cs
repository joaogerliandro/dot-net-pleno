using StallosDotnetPleno.Application.Interfaces;
using StallosDotnetPleno.Domain.Entities;
using StallosDotnetPleno.Domain.Interfaces;
using StallosDotnetPleno.Infrastructure.Interfaces;
using System.Linq.Expressions;

namespace StallosDotnetPleno.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IRepository<Person> _repository;
        private readonly IValidator<Person> _validator;

        public PersonService(IRepository<Person> repository, IValidator<Person> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Person> GetByIdAsync(long id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(Person person)
        {
            person.SetValidator(_validator);
            person.Validate();

            if(!person.IsValid)
            {
                // Notify
            }

            await _repository.AddAsync(person);
        }

        public async Task UpdateAsync(Person person)
        {
            person.SetValidator(_validator);
            person.Validate();

            if (!person.IsValid)
            {
                // Notify
            }

            await _repository.UpdateAsync(person);
        }

        public async Task DeleteAsync(long id)
        {
            var person = await _repository.GetByIdAsync(id);

            if (person == null)
            {
                // Notify
            }

            await _repository.DeleteAsync(person);
        }

        public async Task<IEnumerable<Person>> FindAsync(Expression<Func<Person, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }
    }
}
