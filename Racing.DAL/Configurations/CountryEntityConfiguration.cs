using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Racing.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Racing.DAL.Configurations
{
    class CountryEntityConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            // Properties
            builder.Property(c => c.Name)
                .IsRequired();
        }
    }
}