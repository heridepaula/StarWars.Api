using MediatR;
using StarWars.Application.Shared.Domain.Entities;
using StarWars.Application.Shared.Domain.Models;

namespace StarWars.Application.Features.LoadPlanetById.Models
{
    public class LoadPlanetByIdInput : IRequest<Output<Planet>>
    {
        public long Id { get; set; }

        public LoadPlanetByIdInput(long id)
        {
            Id = id;
        }
    }
}
