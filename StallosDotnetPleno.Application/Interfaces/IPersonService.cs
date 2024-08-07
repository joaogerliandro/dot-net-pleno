﻿using StallosDotnetPleno.Application.ResultObjects;
using StallosDotnetPleno.Domain.Entities;

namespace StallosDotnetPleno.Application.Interfaces
{
    public interface IPersonService
    {
        Task<ContentResult> GetAllAsync();
        Task<ContentResult> GetByIdAsync(long id);
        Task<ContentResult> AddAsync(Person person);
        Task<ContentResult> UpdateAsync(long id, Person person);
        Task<BaseResult> DeleteAsync(long id);
    }
}
