using MediatR;
using Microsoft.AspNetCore.Mvc;
using StarWars.Application.Features.DeletePlanetById.Models;
using StarWars.Application.Features.GetPlanetById.Models;
using StarWars.Application.Features.GetPlanetByName.Models;
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
        [ProducesResponseType(typeof(Output<Planet>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
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

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(Output<Planet>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetPlanetByIdAsync([FromRoute] long id, CancellationToken cancellationToken)
        {
            try
            {
                var input = new GetPlanetByIdInput(id);
                var planet = await _mediator.Send(input, cancellationToken);

                if (!planet?.Success ?? false)
                    return NotFound(planet);

                return Ok(planet);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{name:alpha}")]
        [ProducesResponseType(typeof(Output<Planet>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetPlanetByNameAsync([FromRoute] string name, CancellationToken cancellationToken)
        {
            try
            {
                var input = new GetPlanetByNameInput(name);
                var planet = await _mediator.Send(input, cancellationToken);

                if (!planet?.Success ?? false)
                    return NotFound(planet);

                return Ok(planet);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(typeof(Output<Planet>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeletePlanetByIdAsync([FromRoute] long id, CancellationToken cancellationToken)
        {
            try
            {
                var input = new DeletePlanetByIdInput(id);
                var hasBeenDeleted = await _mediator.Send(input, cancellationToken);

                if (!hasBeenDeleted)
                    return NotFound();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
