using System.Net;
using Hospital.Application.Features.Perfiles.Queries.GetPerfilList;
using Hospital.Domain;
using MediatR;
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

    [HttpGet("list", Name = "GetPerfilList")]
    [ProducesResponseType(typeof(IEnumerable<Perfil>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Perfil>>> GetPerfilList()
    {
        var query = new GetPerfilListQuery();
        var perfiles = await this._mediator.Send(query);
        return Ok(perfiles);
    }
}