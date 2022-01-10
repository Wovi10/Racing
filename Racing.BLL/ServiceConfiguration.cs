using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Racing.BLL.Services;
using Racing.BLL.Services.Interface;

namespace Racing.BLL
{
    public static class ServiceConfiguration
    {
        public static void ConfigureBLServices(this IServiceCollection services)
        {
            services.AddScoped<ICircuitService, CircuitService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IPilotService, PilotService>();
            services.AddScoped<IRaceService, RaceService>();
            services.AddScoped<ISeasonService, SeasonService>();
            services.AddScoped<ISeriesService, SeriesService>();
            services.AddScoped<ITeamParticipantService, TeamParticipantService>();
            services.AddScoped<ITeamService, TeamService>();
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}