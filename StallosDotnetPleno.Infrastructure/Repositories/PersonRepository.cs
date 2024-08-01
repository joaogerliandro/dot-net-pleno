using Microsoft.EntityFrameworkCore;
using StallosDotnetPleno.Domain.Entities;
using StallosDotnetPleno.Infrastructure.Interfaces;
using StallosDotnetPleno.Infrastructure.Data;

namespace StallosDotnetPleno.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
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
            return await _dbSet
                .Include(person => person.RealType)
                .Include(person => person.Addresses)
                .SingleOrDefaultAsync(person => person.Id == id);
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _dbSet
                .Include(person => person.RealType) 
                .Include(person => person.Addresses) 
                .ToListAsync();
        }

        public async Task<Person> GetByDocumentAsync(string document)
        {
            return await _dbSet
                .Include(person => person.RealType) 
                .Include(person => person.Addresses)
                .SingleOrDefaultAsync(person => person.Document == document);
        }

        public async Task UpdateAsync(long personId, Person entity)
        {
            var existingPerson = await GetByIdAsync(personId);

            foreach(var address in existingPerson.Addresses)
            {
                _context.Entry(address).State = EntityState.Deleted;
            }

            existingPerson.UpdateEntity(entity);

            if (existingPerson.Document != entity.Document)
            {
                existingPerson.UpdateDocument(entity.Document);
            }

            foreach (var address in entity.Addresses)
            {
                address.Persons.Add(existingPerson);
                _context.Addresses.Add(address);
            }

            _context.Entry(existingPerson).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Person entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
