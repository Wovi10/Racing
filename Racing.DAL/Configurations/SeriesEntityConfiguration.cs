using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Racing.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Racing.DAL.Configurations
{
    class SeriesEntityConfiguration : IEntityTypeConfiguration<Series>
    {
        public void Configure(EntityTypeBuilder<Series> builder)
        {
            // using 's' for Series

            // Properties
            builder.Property(s => s.Name)
                .IsRequired();

            builder.Property(s => s.StartDate)
                .IsRequired();

            builder.Property(s => s.EndDate);

            builder.Property(s => s.SortingOrder);

            // Indexes
            builder.HasIndex(s => s.Name)
                .IsUnique();

            builder.HasIndex(s => s.SortingOrder);
        }
    }
}