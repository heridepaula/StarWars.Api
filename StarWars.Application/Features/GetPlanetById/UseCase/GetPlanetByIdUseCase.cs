using MediatR;
using Microsoft.Extensions.Logging;
using StarWars.Application.Features.GetFilmById.UseCase;
using StarWars.Application.Features.GetPlanetById.Models;
using StarWars.Application.Shared.Domain.Entities;
using StarWars.Application.Shared.Domain.Interfaces;
using StarWars.Application.Shared.Domain.Models;

namespace StarWars.Application.Features.GetPlanetById.UseCase
{
    public class GetPlanetByIdUseCase : IRequestHandler<GetPlanetByIdInput, Output<Planet>>
    {
        private ILogger<GetPlanetByIdUseCase> _logger;
        private readonly IStarWarsRepository _starWarsRepository;

        public GetPlanetByIdUseCase(ILogger<GetPlanetByIdUseCase> logger,
                                    IStarWarsRepository starWarsRepository)
        {
            _logger = logger;
            _starWarsRepository = starWarsRepository;
        }

        public async Task<Output<Planet>> Handle(GetPlanetByIdInput request, CancellationToken cancellationToken)
        {
            try
            {
                var planet = await _starWarsRepository.GetPlanetByIdAsync(request.Id, cancellationToken);

                return new Output<Planet>(planet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Event} - {Message}]", nameof(GetPlanetByIdUseCase), ex.Message);
                throw;
            }
        }
    }
}
