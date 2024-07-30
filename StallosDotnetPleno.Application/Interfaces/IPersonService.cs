using StallosDotnetPleno.Application.ResultObjects;
using StallosDotnetPleno.Domain.Entities;
using System.Linq.Expressions;

namespace StallosDotnetPleno.Application.Interfaces
{
    public interface IPersonService
    {
        Task<ContentResult> GetAllAsync();
        Task<ContentResult> GetByIdAsync(long id);
        Task<ContentResult> AddAsync(Person person);
        Task<ContentResult> UpdateAsync(Person person);
        Task<BaseResult> DeleteAsync(long id);
    }
}
