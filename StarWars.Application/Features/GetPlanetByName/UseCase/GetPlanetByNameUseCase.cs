using MediatR;
using StarWars.Application.Features.GetPlanetByName.Models;
using StarWars.Application.Shared.Domain.Entities;
using StarWars.Application.Shared.Domain.Interfaces;
using StarWars.Application.Shared.Domain.Models;

namespace StarWars.Application.Features.GetPlanetByName.UseCase
{
    public class GetPlanetByNameUseCase : IRequestHandler<GetPlanetByNameInput, Output<IEnumerable<Planet>>>
    {
        private readonly IStarWarsRepository _starWarsRepository;

        public GetPlanetByNameUseCase(IStarWarsRepository starWarsRepository)
        {
            _starWarsRepository = starWarsRepository;
        }

        public async Task<Output<IEnumerable<Planet>>> Handle(GetPlanetByNameInput request, CancellationToken cancellationToken)
        {
            var result = new Output<IEnumerable<Planet>>();

            try
            {
                var planets = await _starWarsRepository.GetPlanetByNameAsync(request.Name, cancellationToken);
                result.AddResult(planets);
                
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
