using MediatR;
using StarWars.Application.Features.FetchPlanets.Models;
using StarWars.Application.Shared.Domain.Entities;
using StarWars.Application.Shared.Domain.Interfaces;
using StarWars.Application.Shared.Domain.Models;

namespace StarWars.Application.Features.FetchPlanets.UseCase
{
    public class FetchPlanetsUseCase : IRequestHandler<FetchPlanetsInput, Output<IEnumerable<Planet>>>
    {
        private readonly IStarWarsRepository _starWarsRepository;

        public FetchPlanetsUseCase(IStarWarsRepository starWarsRepository)
        {
            _starWarsRepository = starWarsRepository;
        }

        public async Task<Output<IEnumerable<Planet>>> Handle(FetchPlanetsInput request, CancellationToken cancellationToken)
        {
            var output = new Output<IEnumerable<Planet>>();
            try
            {
                var planets = await _starWarsRepository.FetchPlanetsAsync(cancellationToken);

                output.AddResult(planets);

                return output;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
