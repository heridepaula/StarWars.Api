using MediatR;
using Microsoft.AspNetCore.Mvc;
using StarWars.Application.Features.LoadPlanetById.Models;
using StarWars.Application.Shared.Domain.Entities;
using StarWars.Application.Shared.Domain.Models;
using System.Net;

namespace StarWars.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PlanetsController : Controller
    {
        private readonly IMediator _mediator;

        public PlanetsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{id}")]
        [ProducesResponseType(typeof(Output<Planet>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> LoadPlanetByIdAsync([FromRoute] long id, CancellationToken cancellationToken)
        {
            try
            {
                var input = new LoadPlanetByIdInput(id);
                return Ok(await _mediator.Send(input, cancellationToken));
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
