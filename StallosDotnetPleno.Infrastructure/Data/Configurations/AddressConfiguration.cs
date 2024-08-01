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

            builder.HasKey(address => address.Id);

            builder.Property(address => address.ZipCode)
                .HasColumnName("CEP")
                .IsRequired()
                .HasMaxLength(8);

            builder.Property(address => address.Street)
                .HasColumnName("LOGRADOURO")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(address => address.Number)
                .HasColumnName("NUMERO")
                .HasMaxLength(7);

            builder.Property(address => address.District)
                .HasColumnName("BAIRRO")
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(address => address.City)
                .HasColumnName("CIDADE")
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(address => address.StateCode)
                .HasColumnName("UF")
                .IsRequired()
                .HasMaxLength(2);

            builder.HasIndex(address => address.ZipCode);
        }
    }
}
