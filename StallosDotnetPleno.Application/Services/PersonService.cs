using StallosDotnetPleno.Application.Interfaces;
using StallosDotnetPleno.Application.ResultObjects;
using StallosDotnetPleno.Domain.Entities;
using StallosDotnetPleno.Infrastructure.Interfaces;
using FluentValidation;
using StallosDotnetPleno.Domain.Enums;
using StallosDotnetPleno.Application.Mappers;

namespace StallosDotnetPleno.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;
        private readonly IPersonTypeRepository _personTypeRepository;
        private readonly IValidator<Person> _validator;

        public PersonService(IPersonRepository repository, IPersonTypeRepository personTypeRepository, IValidator<Person> validator)
        {
            _repository = repository;
            _personTypeRepository = personTypeRepository;
            _validator = validator;
        }

        public async Task<ContentResult> GetAllAsync()
        {
            var persons = await _repository.GetAllAsync();

            if (persons == null || !persons.Any())
            {
                return new ContentResult
                {
                    Success = false,
                    Message = "No persons available"
                };
            }

            var personDtos = persons.ToDtoList();

            return new ContentResult
            {
                Success = true,
                Message = "Persons retrieved successfully.",
                Content = personDtos
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

            var personDto = person.ToDto();

            return new ContentResult
            {
                Success = true,
                Message = "Person found.",
                Content = personDto
            };
        }

        public async Task<ContentResult> AddAsync(Person person)
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

            PersonTypeEnum personTypeEnum = (PersonTypeEnum)Enum.Parse(typeof(PersonTypeEnum), person.Type);

            PersonType dbPersonType = await _personTypeRepository.GetByTypeAsync(personTypeEnum); // Get the current Type ID

            if (dbPersonType == null) // If not exists
            {
                await _personTypeRepository.AddAsync(new PersonType(personTypeEnum)); // Add enum to database

                dbPersonType = await _personTypeRepository.GetByTypeAsync(personTypeEnum); // Get inserted enum with ID
            }

            person.PrepareToDatabase(dbPersonType);

            Person personRegistry = await _repository.GetByDocumentAsync(person.Document);

            if (personRegistry != null)
            {
                return new ContentResult
                {
                    Success = true,
                    Message = String.Format("The document is already attached to another person. Try with another credential.")
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

        public async Task<ContentResult> UpdateAsync(long personId, Person person)
        {
            var personToUpdate = await _repository.GetByIdAsync(personId);

            if (personToUpdate == null)
            {
                return new ContentResult
                {
                    Success = false,
                    Message = String.Format("Person with id {0} not found.", personId)
                };
            }

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

            PersonTypeEnum personTypeEnum = (PersonTypeEnum)Enum.Parse(typeof(PersonTypeEnum), person.Type);

            PersonType dbPersonType = await _personTypeRepository.GetByTypeAsync(personTypeEnum); // Get the current Type ID

            if (dbPersonType == null) // If not exists
            {
                await _personTypeRepository.AddAsync(new PersonType(personTypeEnum)); // Add enum to database

                dbPersonType = await _personTypeRepository.GetByTypeAsync(personTypeEnum); // Get inserted enum with ID
            }

            person.PrepareToDatabase(dbPersonType);

            if (personToUpdate.Document != person.Document)
            {
                Person personRegistry = await _repository.GetByDocumentAsync(person.Document);

                if (personRegistry != null)
                {
                    return new ContentResult
                    {
                        Success = true,
                        Message = String.Format("The document is already attached to another person. Try with another credential.")
                    };
                }
            }

            await _repository.UpdateAsync(personId, person); // Update the Person Info

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
