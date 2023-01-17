using MediatR;
using StarWars.Application.Shared.Domain.Entities;
using StarWars.Application.Shared.Domain.Models;

namespace StarWars.Application.Features.GetPlanetByName.Models
{
    public class GetPlanetByNameInput : IRequest<Output<IEnumerable<Planet>>>
    {
        public string Name { get; set; }

        public GetPlanetByNameInput(string name)
        {
            Name = name;
        }   
    }
}
