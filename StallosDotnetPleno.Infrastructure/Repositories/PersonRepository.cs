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
                .Include(person => person.PublicLists)
                .SingleOrDefaultAsync(person => person.Id == id);
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _dbSet
                .Include(person => person.RealType) 
                .Include(person => person.Addresses)
                .Include(person => person.PublicLists)
                .ToListAsync();
        }

        public async Task<Person> GetByDocumentAsync(string document)
        {
            return await _dbSet
                .Include(person => person.RealType) 
                .Include(person => person.Addresses)
                .Include(person => person.PublicLists)
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

        public async Task UpdatePersonListAsync(long personId, List<PublicList> personLists)
        {
            var existingPerson = await GetByIdAsync(personId);

            if(existingPerson.PublicLists != null)
            {
                foreach (var publicList in existingPerson.PublicLists)
                {
                    _context.Entry(publicList).State = EntityState.Deleted;
                }
            }

            existingPerson.UpdatePublicLists(personLists);

            _context.Entry(existingPerson).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long personId)
        {
            var person = await _context.Persons
                .Include(person => person.Addresses)
                .FirstOrDefaultAsync(p => p.Id == personId);

            _context.Addresses.RemoveRange(person.Addresses);

            _context.Persons.Remove(person);

            await _context.SaveChangesAsync();
        }
    }
}
