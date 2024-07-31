using StallosDotnetPleno.Domain.Entities;
using StallosDotnetPleno.Domain.Enums;

namespace StallosDotnetPleno.Infrastructure.Interfaces
{
    public interface IPersonTypeRepository
    {
        Task<PersonType> GetByIdAsync(long id);
        Task<PersonType> GetByTypeAsync(PersonTypeEnum type);
        Task AddAsync(PersonType personType);
    }
}
