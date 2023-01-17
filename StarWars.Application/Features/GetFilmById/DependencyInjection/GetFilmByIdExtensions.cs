using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StarWars.Application.Features.GetFilmById.UseCase;
using System.Reflection;

namespace StarWars.Application.Features.GetFilmById.DependencyInjection
{
    public static class GetFilmByIdExtensions
    {
        public static IServiceCollection AddGetFilmByIdExtensions(this IServiceCollection services) =>
            services
                .AddMediatRExtensions();

        private static IServiceCollection AddMediatRExtensions(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetFilmByIdUseCase).GetTypeInfo().Assembly);
            return services;
        }
    }
}
