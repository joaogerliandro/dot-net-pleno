﻿using StallosDotnetPleno.Domain.Entities;
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
                .HasColumnName("NOME")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(p => p.Document)
                .HasColumnName("DOCUMENTO")
                .IsRequired()
                .HasMaxLength(255);

            builder.HasIndex(p => p.Document)
                .IsUnique();

            builder.Property(p => p.PersonTypeId)
                .HasColumnName("ID_TIPO_PESSOA")
                .IsRequired();

            builder.HasOne(p => p.Type)
                .WithMany()
                .HasForeignKey(p => p.PersonTypeId);

            builder.HasMany(p => p.Addresses)
                .WithMany(a => a.Persons)
                .UsingEntity<Dictionary<string, object>>(
                    "TB_PESSOA_ENDERECO",
                    j => j.HasOne<Address>().WithMany().HasForeignKey("ID_ENDERECO"),
                    j => j.HasOne<Person>().WithMany().HasForeignKey("ID_PESSOA"),
                    j => j.HasKey("ID_PESSOA", "ID_ENDERECO"));
        }
    }
}