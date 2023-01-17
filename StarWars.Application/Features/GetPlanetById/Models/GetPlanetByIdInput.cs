using MediatR;
using StarWars.Application.Shared.Domain.Entities;
using StarWars.Application.Shared.Domain.Models;

namespace StarWars.Application.Features.GetPlanetById.Models
{
    public class GetPlanetByIdInput : IRequest<Output<Planet>>
    {
        public long Id { get; set; }

        public GetPlanetByIdInput(long id)
        {
            Id = id;
        }
    }
}
