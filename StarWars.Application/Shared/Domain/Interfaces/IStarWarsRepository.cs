using StarWars.Application.Shared.Domain.Entities;

namespace StarWars.Application.Shared.Domain.Interfaces
{
    public interface IStarWarsRepository
    {
        Task<bool> CreatePlanetAsync(Planet planet, CancellationToken cancellationToken);
        Task<Planet> GetPlanetByIdAsync(long id, CancellationToken cancellationToken);
        Task<Film> GetFilmById(long id, CancellationToken cancellationToken);
        Task<IEnumerable<Planet>> GetPlanetByNameAsync(string name, CancellationToken cancellationToken);
        Task<bool> DeletePlanetByIdAsync(Planet planet, CancellationToken cancellationToken);
        Task<IEnumerable<Planet>> FetchPlanetsAsync(CancellationToken cancellationToken);
    }
}
