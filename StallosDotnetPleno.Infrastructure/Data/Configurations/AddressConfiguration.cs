using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using StallosDotnetPleno.Domain.Entities;

namespace StallosDotnetPleno.Infrastructure.Data.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("TB_ENDERECO");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.ZipCode)
                .IsRequired()
                .HasMaxLength(8);

            builder.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(a => a.Number)
                .HasMaxLength(7);

            builder.Property(a => a.District)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(a => a.StateCode)
                .IsRequired()
                .HasMaxLength(2);

            builder.HasIndex(a => a.ZipCode);
        }
    }
}
