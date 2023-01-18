using MediatR;
using Microsoft.AspNetCore.Mvc;
using StarWars.Application.Features.DeletePlanetById.Models;
using StarWars.Application.Features.FetchPlanets.Models;
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
        private ILogger<PlanetsController> _logger;
        private readonly IMediator _mediator;

        public PlanetsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Carrega um planeta pelo ID diretamente da Api https://swapi.dev/ para o base de dados local.
        /// </summary>
        /// <param name="id">ID do planeta</param>
        /// <returns>As informações do planeta requisitado</returns>
        /// <response code="200">Retorna as informações do planeta</response>
        /// <response code="500">Internal Server Error</response>
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

        /// <summary>
        /// Retorna um planeta pelo ID diretamente da base de dados.
        /// </summary>
        /// <param name="id">ID do planeta</param>
        /// <returns>As informações do planeta</returns>
        /// <response code="200">Retorna as informações do planeta</response>
        /// <response code="404">Retorna quando o planeta não for encontrados na base de dados</response>
        /// <response code="500">Internal Server Error</response>
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

        /// <summary>
        /// Retorna um planeta pelo nome diretamente da base de dados.
        /// </summary>
        /// <param name="name">Nome ou parte do nome do planeta</param>
        /// <returns>As informações do planeta</returns>
        /// <response code="200">Retorna as informações do planeta</response>
        /// <response code="404">Retorna quando o planeta não for encontrado na base de dados</response>
        /// <response code="500">Internal Server Error</response>
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

        /// <summary>
        /// Exclui um planeta a partir de seu ID
        /// </summary>
        /// <param name="id">ID do planeta</param>
        /// <response code="204">Retorna quando o planeta for excluído da base de dados</response>
        /// <response code="404">Retorna quando o planeta não for encontrado na base de dados</response>
        /// <response code="500">Internal Server Error</response>
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

        /// <summary>
        /// Busca a lista de planetas cadastrados na base de dados
        /// </summary>
        /// <response code="200">Retorna a lista de planetas cadastrados na base de dados</response>
        /// <response code="204">Retorna quando não existirem planetas cadastrados na base de dados</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [ProducesResponseType(typeof(Output<IEnumerable<Planet>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> FetchPlanetsAsync(CancellationToken cancellationToken)
        {
            try
            {
                var input = new FetchPlanetsInput();
                var planets = await _mediator.Send(input, cancellationToken);

                if (!planets?.Success ?? false)
                    return NoContent();

                return Ok(planets);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
