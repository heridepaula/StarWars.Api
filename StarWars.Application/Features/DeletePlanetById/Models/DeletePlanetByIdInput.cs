using MediatR;

namespace StarWars.Application.Features.DeletePlanetById.Models
{
    public class DeletePlanetByIdInput : IRequest<bool>
    {
        public long Id { get; set; }

        public DeletePlanetByIdInput(long id) 
        {
            Id = id;
        }
    }
}
