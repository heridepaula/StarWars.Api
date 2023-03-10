using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StarWars.Application.Features.DeletePlanetById.UseCase;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace StarWars.Application.Features.DeletePlanetById.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class DeletePlanetByIdExtensions
    {
        public static IServiceCollection AddDeletePlanetByIdExtensions(this IServiceCollection services) =>
            services
                .AddMediatRExtensions();

        private static IServiceCollection AddMediatRExtensions(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DeletePlanetByIdUseCase).GetTypeInfo().Assembly);
            return services;
        }
    }
}
