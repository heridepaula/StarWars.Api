using MediatR;
using StarWars.Application.Shared.Domain.Entities;
using StarWars.Application.Shared.Domain.Models;

namespace StarWars.Application.Features.GetFilmById.Models
{
    public class GetFilmByIdInput : IRequest<Output<Film>>
    {
        public long Id { get; set; }

        public GetFilmByIdInput(long id)
        { 
            Id = id; 
        }
    }
}
