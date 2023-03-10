using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StarWars.Application.Features.GetPlanetById.UseCase;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace StarWars.Application.Features.GetPlanetById.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class GetPlanetByIdExtensions
    {
        public static IServiceCollection AddGetPlanetByIdExtensions(this IServiceCollection services) =>
            services
                .AddMediatRExtensions();

        private static IServiceCollection AddMediatRExtensions(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetPlanetByIdUseCase).GetTypeInfo().Assembly);
            return services;
        }
    }
}
