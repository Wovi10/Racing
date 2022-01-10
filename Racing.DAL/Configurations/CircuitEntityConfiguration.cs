using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Racing.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Racing.DAL.Configurations
{
    class CircuitEntityConfiguration : IEntityTypeConfiguration<Circuit>
    {
        public void Configure(EntityTypeBuilder<Circuit> builder)
        {
            // Properties
            builder.Property(c => c.Name)
                .IsRequired();

            builder.Property(c => c.Length)
                .IsRequired();

            builder.Property(c => c.CountryId)
                .IsRequired();

            builder.Property(c => c.State)
                .IsRequired();

            builder.Property(c => c.Street)
                .IsRequired();

            builder.Property(c => c.Number)
                .IsRequired();

            // Indexes
            builder.HasIndex(c => c.Name)
                .IsUnique();
        }
    }
}