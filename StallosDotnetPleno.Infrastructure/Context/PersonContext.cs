using StallosDotnetPleno.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace StallosDotnetPleno.Infrastructure.Context
{
    public class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonContext).Assembly);
        }
    }
}
