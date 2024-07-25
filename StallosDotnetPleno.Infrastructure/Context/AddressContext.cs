using StallosDotnetPleno.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace StallosDotnetPleno.Infrastructure.Context
{
    public class AddressContext : DbContext
    {
        public AddressContext(DbContextOptions<AddressContext> options) : base(options) { }

        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AddressContext).Assembly);
        }
    }
}
