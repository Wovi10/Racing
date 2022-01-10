using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Racing.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Racing.DAL.Configurations
{
    class SeasonEntityConfiguration : IEntityTypeConfiguration<Season>
    {
        public void Configure(EntityTypeBuilder<Season> builder)
        {
            // using 's' for Season
            builder.HasKey(s => s.Id);

            // Properties
            builder.Property(s => s.SeriesId);

            builder.Property(s => s.Name)
                .IsRequired();

            builder.Property(s => s.StartDate)
                .IsRequired();
            
            builder.Property(s => s.EndDate)
                .IsRequired();

            builder.Property(s => s.Active)
                .IsRequired();

            // Indexes
            builder.HasIndex(s => s.StartDate);
            
            builder.HasIndex(s => new {SeriesId = s.SeriesId, s.Name })
                .IsUnique();
        }
    }
}
