using StallosDotnetPleno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Data.Configurations
{
    public class PublicListConfiguration : IEntityTypeConfiguration<PublicList>
    {
        public void Configure(EntityTypeBuilder<PublicList> builder)
        {
            builder.ToTable("TB_PESSOA_LISTA");

            builder.HasKey(publicList => publicList.Id);

            builder.Property(publicList => publicList.ListName)
                .HasColumnName("LISTA")
                .IsRequired();

            builder.HasOne(publicList => publicList.Person)
                .WithMany(person => person.PublicLists)
                .HasForeignKey("ID_PESSOA")
                .IsRequired();
        }
    }
}