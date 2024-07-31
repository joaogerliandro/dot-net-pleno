using StallosDotnetPleno.Domain.Entities;
using StallosDotnetPleno.Infrastructure.Interfaces;
using StallosDotnetPleno.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using StallosDotnetPleno.Domain.Enums;

namespace StallosDotnetPleno.Infrastructure.Repositories
{
    public class PersonTypeRepository : IPersonTypeRepository
    {
        private readonly Context _context;

        public PersonTypeRepository(Context context)
        {
            _context = context;
        }

        public async Task<PersonType> GetByIdAsync(long id)
        {
            return await _context.PersonTypes.FindAsync(id);
        }

        public async Task<PersonType> GetByTypeAsync(PersonTypeEnum type)
        {
            return await _context.PersonTypes
                .FirstOrDefaultAsync(personType => personType.Type == type);
        }

        public async Task AddAsync(PersonType personType)
        {
            await _context.PersonTypes.AddAsync(personType);
            await _context.SaveChangesAsync();
        }
    }
}