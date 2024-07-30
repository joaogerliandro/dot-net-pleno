using StallosDotnetPleno.Application.Interfaces;
using StallosDotnetPleno.Application.ResultObjects;
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

        public async Task<ContentResult> GetAllAsync()
        {
            var persons = await _repository.GetAllAsync();

            if (persons == null || !persons.Any())
            {
                return new ContentResult { 
                    Success = false, 
                    Message = "No persons available"
                };
            }

            return new ContentResult
            {
                Success = true,
                Message = "Persons retrieved successfully.",
                Content = persons
            };
        }

        public async Task<ContentResult> GetByIdAsync(long id)
        {
            var person = await _repository.GetByIdAsync(id);

            if (person == null)
            {
                return new ContentResult
                {
                    Success = false,
                    Message = String.Format("Person with id {0} not found.", id)
                };
            }

            return new ContentResult
            {
                Success = true,
                Message = "Person found.",
                Content = person
            };
        }

        public async Task<ContentResult> AddAsync(Person person)
        {
            person.SetValidator(_validator);
            person.Validate();

            if(!person.IsValid)
            {
                return new ContentResult
                {
                    Success = false,
                    Message = String.Format("Informed person is invalid. Get more details at Notifications below."),
                    Notifications = person.GetNotifications()
                };
            }

            await _repository.AddAsync(person);

            return new ContentResult
            {
                Success = true,
                Message = "Person created successfully.",
                Content = new { PersonId = person.Id }
            };
        }

        public async Task<ContentResult> UpdateAsync(Person person)
        {
            person.SetValidator(_validator);
            person.Validate();

            if (!person.IsValid)
            {
                return new ContentResult
                {
                    Success = false,
                    Message = String.Format("Informed person is invalid. Get more details at Notifications below."),
                    Notifications = person.GetNotifications()
                };
            }

            await _repository.UpdateAsync(person);

            return new ContentResult
            {
                Success = true,
                Message = "Person updated successfully."
            };
        }

        public async Task<BaseResult> DeleteAsync(long id)
        {
            var person = await _repository.GetByIdAsync(id);

            if (person == null)
            {
                return new BaseResult
                {
                    Success = false,
                    Message = String.Format("Person with id {0} not found.", id)
                };
            }

            await _repository.DeleteAsync(person);

            return new BaseResult
            {
                Success = true,
                Message = "Person deleted successfully.",
            };
        }
    }
}
