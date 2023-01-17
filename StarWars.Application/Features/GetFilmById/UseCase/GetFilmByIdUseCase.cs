using MediatR;
using StarWars.Application.Features.GetFilmById.Models;
using StarWars.Application.Shared.Domain.Entities;
using StarWars.Application.Shared.Domain.Interfaces;
using StarWars.Application.Shared.Domain.Models;

namespace StarWars.Application.Features.GetFilmById.UseCase
{
    public class GetFilmByIdUseCase : IRequestHandler<GetFilmByIdInput, Output<Film>>
    {
        private readonly IStarWarsApiService _starWarsApiService;
        private readonly IStarWarsRepository _starWarsRepository;

        public GetFilmByIdUseCase(IStarWarsApiService starWarsApiService, 
                                  IStarWarsRepository starWarsRepository)
        {
            _starWarsApiService = starWarsApiService;
            _starWarsRepository = starWarsRepository;
        }

        public async Task<Output<Film>> Handle(GetFilmByIdInput request, CancellationToken cancellationToken)
        {
            var film = await _starWarsRepository.GetFilmById(request.Id, cancellationToken);

            if (film is not null) 
            {
                return new(film);
            }

            var filmApiResponse = await _starWarsApiService.GetFilmByIdAsync(request.Id, cancellationToken);

            film = new Film
            {
                Id = request.Id,
                Director = filmApiResponse.Director,
                Name = filmApiResponse.Title,
                ReleaseDate = filmApiResponse.ReleaseDate
            };

            return new(film);
        }
    }
}
