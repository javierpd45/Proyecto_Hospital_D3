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

    public PerfilController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [AllowAnonymous] //Esto es para no tener que loguearse para poder ver la lista de Perfiles
    [HttpGet("list", Name = "GetPerfilList")]
    [ProducesResponseType(typeof(IReadOnlyList<Perfil>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyList<Perfil>>> GetPerfilList()
    {
        var query = new GetPerfilListQuery();
        var perfiles = await this._mediator.Send(query);
        return Ok(perfiles);
    }
}