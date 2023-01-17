using MediatR;
using StarWars.Application.Shared.Domain.Entities;
using StarWars.Application.Shared.Domain.Models;

namespace StarWars.Application.Features.FetchPlanets.Models
{
    public class FetchPlanetsInput : IRequest<Output<IEnumerable<Planet>>>
    {
    }
}
