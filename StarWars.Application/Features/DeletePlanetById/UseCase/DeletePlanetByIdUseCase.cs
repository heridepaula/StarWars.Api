using MediatR;
using Microsoft.Extensions.Logging;
using StarWars.Application.Features.DeletePlanetById.Models;
using StarWars.Application.Shared.Domain.Interfaces;

namespace StarWars.Application.Features.DeletePlanetById.UseCase
{
    public class DeletePlanetByIdUseCase : IRequestHandler<DeletePlanetByIdInput, bool>
    {
        private readonly ILogger<DeletePlanetByIdUseCase> _logger;
        private readonly IStarWarsRepository _starWarsRepository;

        public DeletePlanetByIdUseCase(ILogger<DeletePlanetByIdUseCase> logger, 
                                       IStarWarsRepository starWarsRepository)
        {
            _logger = logger;
            _starWarsRepository = starWarsRepository;
        }

        public async Task<bool> Handle(DeletePlanetByIdInput request, CancellationToken cancellationToken)
        {
            try
            {
                var planet = await _starWarsRepository.GetPlanetByIdAsync(request.Id, cancellationToken);

                if (planet is null)
                {
                    _logger.LogWarning("[{Event}] - No planets found for Id {Id}", nameof(DeletePlanetByIdUseCase),  request.Id);
                    return false;
                }

                return await _starWarsRepository.DeletePlanetByIdAsync(planet, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Event}] - {Message}", nameof(DeletePlanetByIdUseCase), ex.Message);
                throw;
            }
        }
    }
}
