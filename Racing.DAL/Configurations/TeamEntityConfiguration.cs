using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Racing.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Racing.DAL.Configurations
{
    class TeamEntityConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            // Properties
            builder.Property(t => t.Name)
                .IsRequired();

            // Indexes
            builder.HasIndex(t => t.Name)
                .IsUnique();
        }
    }
}