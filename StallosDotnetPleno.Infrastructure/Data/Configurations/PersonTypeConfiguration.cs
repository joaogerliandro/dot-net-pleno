using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using StallosDotnetPleno.Domain.Entities;

namespace StallosDotnetPleno.Infrastructure.Data.Configurations
{
    public class PersonTypeConfiguration : IEntityTypeConfiguration<PersonType>
    {
        public void Configure(EntityTypeBuilder<PersonType> builder)
        {
            builder.ToTable("TB_TIPO_PESSOA");

            builder.HasKey(personType => personType.Id);

            builder.Property(personType => personType.Type)
                .HasColumnName("TIPO");
        }
    }
}
