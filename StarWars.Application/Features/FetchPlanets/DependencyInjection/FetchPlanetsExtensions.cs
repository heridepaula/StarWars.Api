using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StarWars.Application.Features.FetchPlanets.UseCase;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace StarWars.Application.Features.FetchPlanets.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class FetchPlanetsExtensions
    {
        public static IServiceCollection AddFetchPlanetsExtensions(this IServiceCollection services) =>
            services
                .AddMediatRExtensions();

        private static IServiceCollection AddMediatRExtensions(this IServiceCollection services)
        {
            services.AddMediatR(typeof(FetchPlanetsUseCase).GetTypeInfo().Assembly);
            return services;
        }
    }
}
