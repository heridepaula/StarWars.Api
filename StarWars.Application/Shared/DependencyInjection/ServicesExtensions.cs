using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StarWars.Application.Shared.Domain.Interfaces;
using StarWars.Application.Shared.Services;

namespace StarWars.Application.Shared.DependencyInjection
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServicesExtensions(this IServiceCollection services, IConfiguration config)
            => services
                .AddStarWarsApiService(config);

        private static IServiceCollection AddStarWarsApiService(this IServiceCollection services, IConfiguration config)
        {
            services.AddHttpClient<IStarWarsApiService, StarWarsApiService>("StarWarsApi", client =>
            {
                client.BaseAddress = new Uri(config.GetSection("StarWarsApiBaseUrl").Value);
            });

            services.AddScoped<IStarWarsApiService, StarWarsApiService>();

            return services;
        }
    }
}
