using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StarWars.Application.Features.LoadPlanetById.UseCase;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace StarWars.Application.Features.LoadPlanetById.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class LoadPlanetByIdExtensions
    {
        public static IServiceCollection AddLoadPlanetByIdExtensions(this IServiceCollection services)
            => services
                    .AddMediatRExtensions();

        private static IServiceCollection AddMediatRExtensions(this IServiceCollection services)
        {
            services.AddMediatR(typeof(LoadPlanetByIdUseCase).GetTypeInfo().Assembly);
            return services;
        }
    }
}
