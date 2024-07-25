using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StallosDotnetPleno.Domain.Entities;

namespace StallosDotnetPleno.Infrastructure.Mappings
{
    public class AddressMap : BaseEntityMap<Address>
    {
        public override void Configure(EntityTypeBuilder<Address> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.PostCode)
                .IsRequired()
                .HasMaxLength(8);

            builder.Property(p => p.Street)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(p => p.Number)
                .IsRequired()
                .HasMaxLength(7);

            builder.Property(p => p.City)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(p => p.StateCode)
                .IsRequired()
                .HasMaxLength(2);
        }
    }
}
