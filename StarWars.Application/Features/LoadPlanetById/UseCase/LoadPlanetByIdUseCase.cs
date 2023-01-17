using MediatR;
using StarWars.Application.Features.GetFilmById.Models;
using StarWars.Application.Features.LoadPlanetById.Models;
using StarWars.Application.Shared.Domain.Entities;
using StarWars.Application.Shared.Domain.Interfaces;
using StarWars.Application.Shared.Domain.Models;
using StarWars.Application.Shared.Extensions;
using System.Collections.Concurrent;

namespace StarWars.Application.Features.LoadPlanetById.UseCase
{
    public class LoadPlanetByIdUseCase : IRequestHandler<LoadPlanetByIdInput, Output<Planet>>
    {
        private readonly IMediator _mediator;
        private readonly IStarWarsApiService _starWarsApiService;
        private readonly IStarWarsRepository _starWarsRepository;

        public LoadPlanetByIdUseCase(IMediator mediator,
                                     IStarWarsApiService starWarsApiService, 
                                     IStarWarsRepository starWarsRepository)
        {
            _mediator = mediator;
            _starWarsApiService = starWarsApiService;
            _starWarsRepository = starWarsRepository;
        }

        public async Task<Output<Planet>> Handle(LoadPlanetByIdInput request, CancellationToken cancellationToken)
        {
            try
            {
                var planet = await _starWarsRepository.GetPlanetByIdAsync(request.Id, cancellationToken);

                if (planet is not null)
                    return new Output<Planet>(planet);

                var planetApiResponse = await _starWarsApiService.GetPlanetByIdAsync(request.Id, cancellationToken);

                if (planetApiResponse is null)
                    return new Output<Planet>(planet);

                planet = new Planet
                {
                    Id = request.Id,
                    Climate = planetApiResponse.Climate,
                    Name = planetApiResponse.Name,
                    Terrain = planetApiResponse.Terrain,
                };

                planet.Films = await PopulateFilmsAsync(planetApiResponse.Films);

                await _starWarsRepository.CreatePlanetAsync(planet, cancellationToken);

                return new Output<Planet>(planet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<ICollection<Film>> PopulateFilmsAsync(ICollection<string> links)
        {
            var bag = new ConcurrentBag<Output<Film>>();
            var films = new List<Film>();

            var tasks = links.Select(async link =>
            {
                var id = link.GetRouteParamValue();
                var response = await _mediator.Send(new GetFilmByIdInput(id));
                bag.Add(response);
            });

            await Task.WhenAll(tasks);

            var results = bag.Where(x => x.Data is not null).Select(x => x.Data) ?? new List<Film>();
            films.AddRange(results);

            return films;
        }
    }
}
