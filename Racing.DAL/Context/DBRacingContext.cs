using Microsoft.EntityFrameworkCore;
using Racing.DAL.Models;
using System.Reflection;

namespace Racing.DAL.Context
{
    public class DBRacingContext : DbContext
    {
        public DbSet<Circuit> Circuits { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Pilot> Pilots { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamParticipant> TeamParticipants { get; set; }

        public DBRacingContext(DbContextOptions<DBRacingContext> options) : base(options)
        {
        }

        public DBRacingContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}