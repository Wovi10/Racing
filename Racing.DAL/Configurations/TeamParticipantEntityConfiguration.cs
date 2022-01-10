using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Racing.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Racing.DAL.Configurations
{
    class TeamParticipantEntityConfiguration : IEntityTypeConfiguration<TeamParticipant>
    {
        public void Configure(EntityTypeBuilder<TeamParticipant> builder)
        {
            // Properties
            builder.Property(tp => tp.TeamId);

            builder.Property(tp => tp.RaceId);

            builder.Property(tp => tp.PilotId);

            // Indexes
            builder.HasIndex(tp => new {Pilots = tp.PilotId, Races = tp.RaceId})
                .IsUnique();
        }
    }
}