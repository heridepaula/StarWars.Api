using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StarWars.Application.Shared.Contexts;
using StarWars.Application.Shared.Domain.Interfaces;
using StarWars.Application.Shared.Repositories;

namespace StarWars.Application.Shared.DependencyInjection
{
    public static class DataBaseExtensions
    {
        public static IServiceCollection AddDataBaseExtensions(this IServiceCollection services) =>
            services
                .AddStarWarsRepository()
                .AddPlanetsDbContext();

        private static IServiceCollection AddPlanetsDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlite("Data Source=StarWars.db;Cache=Shared"));

            return services;
        }

        private static IServiceCollection AddStarWarsRepository(this IServiceCollection services)
        {
            services.AddTransient<IStarWarsRepository, StarWarsRepository>();
            return services;
        }
    }
}
