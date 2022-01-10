using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Racing.DAL.Context;
using Racing.DAL.Repositories;
using Racing.DAL.Repositories.Interface;

namespace Racing.DAL
{
    public static class ServiceConfiguration
    {
        public static void ConfigureDalServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DBRacingContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Racing.API"))
            );

            AddScopes(services);
        }

        private static void AddScopes(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICircuitRepository, CircuitRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IPilotRepository, PilotRepository>();
            services.AddScoped<IRaceRepository, RaceRepository>();
            services.AddScoped<ISeasonRepository, SeasonRepository>();
            services.AddScoped<ISeriesRepository, SeriesRepository>();
            services.AddScoped<ITeamParticipantRepository, TeamParticipantRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
        }
    }
}