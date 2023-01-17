using MediatR;
using Microsoft.Extensions.Logging;
using StarWars.Application.Features.GetPlanetById.UseCase;
using StarWars.Application.Features.GetPlanetByName.Models;
using StarWars.Application.Shared.Domain.Entities;
using StarWars.Application.Shared.Domain.Interfaces;
using StarWars.Application.Shared.Domain.Models;

namespace StarWars.Application.Features.GetPlanetByName.UseCase
{
    public class GetPlanetByNameUseCase : IRequestHandler<GetPlanetByNameInput, Output<IEnumerable<Planet>>>
    {
        private readonly ILogger<GetPlanetByNameUseCase> _logger;
        private readonly IStarWarsRepository _starWarsRepository;

        public GetPlanetByNameUseCase(ILogger<GetPlanetByNameUseCase> logger,
                                      IStarWarsRepository starWarsRepository)
        {
            _logger = logger;
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Event} - {Message}]", nameof(GetPlanetByNameUseCase), ex.Message);
                throw;
            }
        }
    }
}
