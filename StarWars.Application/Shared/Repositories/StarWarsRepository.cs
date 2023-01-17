using Microsoft.EntityFrameworkCore;
using StarWars.Application.Shared.Contexts;
using StarWars.Application.Shared.Domain.Entities;
using StarWars.Application.Shared.Domain.Interfaces;

namespace StarWars.Application.Shared.Repositories
{
    public class StarWarsRepository : IStarWarsRepository
    {
        private readonly ApplicationDbContext _context;

        public StarWarsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreatePlanetAsync(Planet planet, CancellationToken cancellationToken)
        {
            _context.Planets.Add(planet);
            return (await _context.SaveChangesAsync(cancellationToken)) > 0;
        }
        public async Task<Planet> GetPlanetByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.Planets
                            .Include(x => x.Films)
                            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Planet>> GetPlanetByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _context.Planets
                            .Include(x => x.Films)
                            .Where(x => EF.Functions.Like(x.Name, $"%{name}%"))
                            .ToListAsync(cancellationToken);
        }

        public async Task<Film> GetFilmById(long id, CancellationToken cancellationToken)
        {
            return await _context.Films
                            .Where(x => x.Id == id)
                            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<bool> DeletePlanetByIdAsync(Planet planet, CancellationToken cancellationToken)
        {
            _context.Planets.Remove(planet);
            return (await _context.SaveChangesAsync(cancellationToken)) > 0;
        }

        public async Task<IEnumerable<Planet>> FetchPlanetsAsync(CancellationToken cancellationToken)
        {
            return await _context.Planets
                        .Include(x => x.Films)
                        .ToListAsync(cancellationToken);
        }
    }
}
