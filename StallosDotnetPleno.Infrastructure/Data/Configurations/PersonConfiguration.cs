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

            builder.HasKey(person => person.Id);

            builder.Property(person => person.Name)
                .HasColumnName("NOME");

            builder.Property(person => person.Document)
                .HasColumnName("DOCUMENTO");

            builder.HasIndex(person => person.Document)
                .IsUnique();

            builder.HasOne(person => person.RealType)
                .WithMany()
                .HasForeignKey("ID_TIPO_PESSOA");

            builder.HasMany(person => person.Addresses)
                .WithMany(address => address.Persons)
                .UsingEntity<Dictionary<string, object>>(
                    "TB_PESSOA_ENDERECO",
                    j => j.HasOne<Address>().WithMany().HasForeignKey("ID_ENDERECO"),
                    j => j.HasOne<Person>().WithMany().HasForeignKey("ID_PESSOA"),
                    j => j.HasKey("ID_PESSOA", "ID_ENDERECO"));
        }
    }
}