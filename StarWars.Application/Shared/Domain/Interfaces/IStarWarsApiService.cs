using StarWars.Application.Shared.Domain.Responses;

namespace StarWars.Application.Shared.Domain.Interfaces
{
    public interface IStarWarsApiService
    {
        Task<PlanetResponse> GetPlanetByIdAsync(long id, CancellationToken cancellationToken);
        Task<FilmResponse> GetFilmByIdAsync(long id, CancellationToken cancellationToken);
    }
}
