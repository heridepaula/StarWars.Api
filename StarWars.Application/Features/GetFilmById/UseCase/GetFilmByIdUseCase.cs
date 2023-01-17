using MediatR;
using Microsoft.Extensions.Logging;
using StarWars.Application.Features.FetchPlanets.UseCase;
using StarWars.Application.Features.GetFilmById.Models;
using StarWars.Application.Shared.Domain.Entities;
using StarWars.Application.Shared.Domain.Interfaces;
using StarWars.Application.Shared.Domain.Models;

namespace StarWars.Application.Features.GetFilmById.UseCase
{
    public class GetFilmByIdUseCase : IRequestHandler<GetFilmByIdInput, Output<Film>>
    {
        private readonly ILogger<GetFilmByIdUseCase> _logger;
        private readonly IStarWarsApiService _starWarsApiService;
        private readonly IStarWarsRepository _starWarsRepository;

        public GetFilmByIdUseCase(ILogger<GetFilmByIdUseCase> logger,
                                  IStarWarsApiService starWarsApiService,
                                  IStarWarsRepository starWarsRepository)
        {
            _logger = logger;
            _starWarsApiService = starWarsApiService;
            _starWarsRepository = starWarsRepository;
        }

        public async Task<Output<Film>> Handle(GetFilmByIdInput request, CancellationToken cancellationToken)
        {
            try
            {
                var film = await _starWarsRepository.GetFilmById(request.Id, cancellationToken);

                if (film is not null)
                {
                    _logger.LogInformation("[{Event}] - Film Id {Id} found in Database", nameof(GetFilmByIdUseCase), request.Id);
                    return new(film);
                }

                var filmApiResponse = await _starWarsApiService.GetFilmByIdAsync(request.Id, cancellationToken);

                if (filmApiResponse is null)
                {
                    _logger.LogWarning("[{Event}] - Film Id {Id} not found in Api", nameof(GetFilmByIdUseCase), request.Id);
                    return new(film);
                }

                film = new Film
                {
                    Id = request.Id,
                    Director = filmApiResponse.Director,
                    Name = filmApiResponse.Title,
                    ReleaseDate = filmApiResponse.ReleaseDate
                };

                return new(film);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Event} - {Message}]", nameof(GetFilmByIdUseCase), ex.Message);
                throw;
            }
        }
    }
}
