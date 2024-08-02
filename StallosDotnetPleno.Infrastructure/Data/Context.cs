using StallosDotnetPleno.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace StallosDotnetPleno.Infrastructure.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<PersonType> PersonTypes { get; set; }

        public DbSet<PublicList> PublicLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
        }
    }
}