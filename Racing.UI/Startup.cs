using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Racing.DTO.CreateDTO;
using Racing.DTO.CreateDTO.FluentValidation;
using Racing.DTO.UpdateDTO;
using Racing.DTO.UpdateDTO.FluentValidation;

namespace Racing.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ResourceConfiguration(services);
            SingletonConfiguration(services);
            AddFluentValidators(services);
        }

        private static void AddFluentValidators(IServiceCollection services)
        {
            services.AddRazorPages().AddFluentValidation();
            AddValidators<CountryCreateDTO, CountryUpdateDTO, CountryCreateDTOValidator, CountryUpdateDTOValidator>(
                services);
            AddValidators<CircuitCreateDTO, CircuitUpdateDTO, CircuitCreateDTOValidator, CircuitUpdateDTOValidator>(
                services);
            AddValidators<PilotCreateDTO, PilotUpdateDTO, PilotCreateDTOValidator, PilotUpdateDTOValidator>(
                services);
            AddValidators<RaceCreateDTO, RaceUpdateDTO, RaceCreateDTOValidator, RaceUpdateDTOValidator>(
                services);
            AddValidators<SeasonCreateDTO, SeasonUpdateDTO, SeasonCreateDTOValidator, SeasonUpdateDTOValidator>(
                services);
            AddValidators<SeriesCreateDTO, SeriesUpdateDTO, SeriesCreateDTOValidator, SeriesUpdateDTOValidator>(
                services);
            AddValidators<SeriesCreateDTO, SeriesUpdateDTO, SeriesCreateDTOValidator, SeriesUpdateDTOValidator>(
                services);
            AddValidators<TeamParticipantsCreateDTO, TeamParticipantsUpdateDTO, TeamParticipantCreateDTOValidator,
                TeamParticipantUpdateDTOValidator>(
                services);
            AddValidators<TeamCreateDTO, TeamUpdateDTO, TeamCreateDTOValidator, TeamUpdateDTOValidator>(
                services);
        }

        private static void
            AddValidators<TCreateDTO, TUpdateDTO, TCreateValDTO, TUpdateValDTO>(IServiceCollection services)
            where TUpdateValDTO : class, IValidator<TUpdateDTO> where TCreateValDTO : class, IValidator<TCreateDTO>
        {
            services.AddTransient<IValidator<TCreateDTO>, TCreateValDTO>();
            services.AddTransient<IValidator<TUpdateDTO>, TUpdateValDTO>();
        }

        private static void SingletonConfiguration(IServiceCollection services)
        {
            services.AddSingleton<HttpClient>();
            services.AddSingleton(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        private static void ResourceConfiguration(IServiceCollection services)
        {
            services.AddLocalization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });
        }
    }
}