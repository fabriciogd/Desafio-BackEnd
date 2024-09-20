using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moto.Api.Extensions;
using Moto.Api.Models;
using Moto.Application.Motorcycles.CreateMotorcycle;
using Moto.Application.Motorcycles.ListMotorcycle;
using System.Net.Mime;

namespace Moto.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v1/[controller]")]
public class MotorcycleController(IMediator mediator): ControllerBase
{
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse<CreateMotorcycleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateMotorcycleCommand command) =>
        (await mediator.Send(command)).ToActionResult();

    [HttpGet()]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyCollection<CreateMotorcycleResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> List([FromQuery] ListMotorcycleQuery query) =>
        (await mediator.Send(query)).ToActionResult();


}
