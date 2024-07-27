using StallosDotnetPleno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Data.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("TB_PESSOA");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(p => p.Document)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasIndex(p => p.Document)
                .IsUnique();

            builder.Property(p => p.Type)
                .HasConversion<int>();

            builder.HasMany(p => p.Addresses)
                .WithMany(a => a.Persons)
                .UsingEntity<Dictionary<string, object>>(
                    "PersonAddress",
                    j => j.HasOne<Address>().WithMany().HasForeignKey("AddressId"),
                    j => j.HasOne<Person>().WithMany().HasForeignKey("PersonId"),
                    j => j.HasKey("PersonId", "AddressId"));
        }
    }
}