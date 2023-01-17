using MediatR;
using Microsoft.Extensions.Logging;
using StarWars.Application.Features.DeletePlanetById.UseCase;
using StarWars.Application.Features.FetchPlanets.Models;
using StarWars.Application.Shared.Domain.Entities;
using StarWars.Application.Shared.Domain.Interfaces;
using StarWars.Application.Shared.Domain.Models;

namespace StarWars.Application.Features.FetchPlanets.UseCase
{
    public class FetchPlanetsUseCase : IRequestHandler<FetchPlanetsInput, Output<IEnumerable<Planet>>>
    {
        private readonly ILogger<FetchPlanetsUseCase> _logger;
        private readonly IStarWarsRepository _starWarsRepository;

        public FetchPlanetsUseCase(ILogger<FetchPlanetsUseCase> logger, 
                                   IStarWarsRepository starWarsRepository)
        {
            _logger = logger;
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Event} - {Message}]", nameof(FetchPlanetsUseCase), ex.Message);
                throw;
            }
        }
    }
}
