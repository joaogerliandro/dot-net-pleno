using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StallosDotnetPleno.Domain.Entities;

namespace StallosDotnetPleno.Infrastructure.Data.Configurations
{
    public class PersonTypeConfiguration : IEntityTypeConfiguration<PersonType>
    {
        public void Configure(EntityTypeBuilder<PersonType> builder)
        {
            builder.ToTable("TB_TIPO_PESSOA");

            builder.HasKey(pt => pt.Id);

            builder.Property(pt => pt.Type)
                .HasConversion<int>()
                .IsRequired();
        }
    }
}