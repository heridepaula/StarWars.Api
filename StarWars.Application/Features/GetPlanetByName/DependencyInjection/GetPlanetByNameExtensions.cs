using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StarWars.Application.Features.GetPlanetByName.UseCase;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace StarWars.Application.Features.GetPlanetByName.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class GetPlanetByNameExtensions
    {
        public static IServiceCollection AddGetPlanetByNameExtensions(this IServiceCollection services) =>
            services
                .AddMediatRExtensions();

        private static IServiceCollection AddMediatRExtensions(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetPlanetByNameUseCase).GetTypeInfo().Assembly);
            return services;
        }
    }
}
