using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StarWars.Application.Shared.Contexts;
using StarWars.Application.Shared.Domain.Interfaces;
using StarWars.Application.Shared.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace StarWars.Application.Shared.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class DataBaseExtensions
    {
        public static IServiceCollection AddDataBaseExtensions(this IServiceCollection services, IConfiguration config) =>
            services
                .AddStarWarsRepository()
                .AddPlanetsDbContext(config);

        private static IServiceCollection AddPlanetsDbContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => 
                {
                    options.UseSqlite(config.GetConnectionString("SQLite"));
                    options.EnableSensitiveDataLogging(false);
                });

            return services;
        }

        private static IServiceCollection AddStarWarsRepository(this IServiceCollection services)
        {
            services.AddTransient<IStarWarsRepository, StarWarsRepository>();
            return services;
        }
    }
}
