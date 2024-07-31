using Microsoft.EntityFrameworkCore;
using StallosDotnetPleno.Domain.Entities;
using StallosDotnetPleno.Infrastructure.Interfaces;
using StallosDotnetPleno.Infrastructure.Data;

namespace StallosDotnetPleno.Infrastructure.Repositories
{
    public class PersonRepository : IRepository<Person>
    {
        private readonly Context _context;

        private readonly DbSet<Person> _dbSet;

        public PersonRepository(Context context)
        {
            _context = context;
            _dbSet = context.Set<Person>();
        }

        public async Task AddAsync(Person entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Person> GetByIdAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task UpdateAsync(Person entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Person entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
