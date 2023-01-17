using MediatR;
using StarWars.Application.Features.DeletePlanetById.Models;
using StarWars.Application.Shared.Domain.Interfaces;
using StarWars.Application.Shared.Domain.Models;

namespace StarWars.Application.Features.DeletePlanetById.UseCase
{
    public class DeletePlanetByIdUseCase : IRequestHandler<DeletePlanetByIdInput, bool>
    {
        private readonly IStarWarsRepository _starWarsRepository;

        public DeletePlanetByIdUseCase(IStarWarsRepository starWarsRepository)
        {
            _starWarsRepository = starWarsRepository;
        }

        public async Task<bool> Handle(DeletePlanetByIdInput request, CancellationToken cancellationToken)
        {
            try
            {
                var planet = await _starWarsRepository.GetPlanetByIdAsync(request.Id, cancellationToken);

                if (planet is null)
                {
                    return false;
                }

                return await _starWarsRepository.DeletePlanetByIdAsync(planet, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
