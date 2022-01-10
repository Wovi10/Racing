using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Racing.DAL.Models;

namespace Racing.DAL.Configurations
{
    class PilotEntityConfiguration : IEntityTypeConfiguration<Pilot>
    {
        public void Configure(EntityTypeBuilder<Pilot> builder)
        {
            // Properties
            builder.Property(p => p.LicensNr)
                .IsRequired();

            builder.Property(t => t.Name)
                .IsRequired();

            builder.Property(p => p.FirstName)
                .IsRequired();

            builder.Property(p => p.NickName);

            builder.Property(p => p.PhotoPath)
                .IsRequired();

            builder.Property(p => p.Gender)
                .IsRequired();

            builder.Property(p => p.Birthdate)
                .IsRequired();

            builder.Property(p => p.Length);

            builder.Property(p => p.Weight);

            // Indexes
            builder.HasIndex(p => p.LicensNr)
                .IsUnique();

            builder.HasIndex(p => p.Name);

            builder.HasIndex(p => p.FirstName);

            builder.HasIndex(p => p.NickName);
        }
    }
}