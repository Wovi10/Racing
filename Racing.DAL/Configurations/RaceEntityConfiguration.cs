using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Racing.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Racing.DAL.Configurations
{
    class RaceEntityConfiguration : IEntityTypeConfiguration<Race>
    {
        public void Configure(EntityTypeBuilder<Race> builder)
        {
            // using 'r' for Race
            builder.HasKey(r => r.Id);

            // Properties
            builder.Property(r => r.SeasonId);

            builder.Property(r => r.CircuitId);

            builder.Property(r => r.Name)
                .IsRequired();

            builder.Property(r => r.StartDate)
                .IsRequired();

            builder.Property(r => r.EndDate)
                .IsRequired();

            // Indexes
            builder.HasIndex(r => r.StartDate);

            builder.HasIndex(r => new {r.SeasonId, r.CircuitId, r.Name})
                .IsUnique();
        }
    }
}