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
                .HasColumnName("CEP")
                .IsRequired()
                .HasMaxLength(8);

            builder.Property(a => a.Street)
                .HasColumnName("LOGRADOURO")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(a => a.Number)
                .HasColumnName("NUMERO")
                .HasMaxLength(7);

            builder.Property(a => a.District)
                .HasColumnName("BAIRRO")
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(a => a.City)
                .HasColumnName("CIDADE")
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(a => a.StateCode)
                .HasColumnName("UF")
                .IsRequired()
                .HasMaxLength(2);

            builder.HasIndex(a => a.ZipCode);
        }
    }
}
