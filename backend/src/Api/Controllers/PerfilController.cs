using System.Net;
using Hospital.Application.Features.Perfiles.Queries.GetPerfilList;
using Hospital.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PerfilController : ControllerBase
{
    private IMediator _mediator;
    
    private ILogger<PerfilController> _logger;

    public PerfilController(IMediator mediator, ILogger<PerfilController> logger)
    {
        this._mediator = mediator;
        this._logger = logger;
    }

    [AllowAnonymous] //Esto es para no tener que loguearse para poder ver la lista de Perfiles
    [HttpGet("list", Name = "GetPerfilList")]
    [ProducesResponseType(typeof(IReadOnlyList<Perfil>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyList<Perfil>>> GetPerfilList()
    {
        try
        {
            var query = new GetPerfilListQuery();
            var perfiles = await _mediator.Send(query);
            return Ok(perfiles);
        }

        catch (Exception ex)
        {
            // Loguear la excepción
            _logger.LogError(ex, "Error en GetPerfilList");
            return StatusCode(500, "Error interno del servidor");
        }
    }
}