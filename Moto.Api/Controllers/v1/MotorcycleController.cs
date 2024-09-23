using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moto.Api.Extensions;
using Moto.Api.Models;
using Moto.Application.Motorcycles.Commands;
using Moto.Application.Motorcycles.Queries;
using Moto.Application.Motorcycles.Responses;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Moto.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/motos")]
public class MotorcycleController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Cadastrar uma nova moto")]
    [SwaggerResponse(StatusCodes.Status201Created, "Moto criada com sucesso")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos", typeof(ApiResponse))]
    public async Task<IActionResult> Create([FromBody] CreateMotorcycle command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result.ToActionResult();
    }

    [HttpGet()]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Consultar motos existentes")]
    [SwaggerResponse(StatusCodes.Status200OK, "Lista das motos", typeof(ApiResponse<List<MotorcycleResponse>>))]
    public async Task<IActionResult> List([FromQuery] GetAllMotrocycles query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return result.ToActionResult();
    }

    [HttpPut("{id}/placa")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Modificar a placa de uma moto")]
    [SwaggerResponse(StatusCodes.Status200OK, "Placa modificada com sucesso", typeof(ApiResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos", typeof(ApiResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Moto não encontrada", typeof(ApiResponse))]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateLicensePlate command, CancellationToken cancellationToken)
    {
        command = command with { Id = id };

        var result = await mediator.Send(command, cancellationToken);
        return result.ToActionResult();
    }

    [HttpGet("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Consultar moto existente por id")]
    [SwaggerResponse(StatusCodes.Status200OK, "Detalhes da moto", typeof(MotorcycleResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Moto não encontrada", typeof(ApiResponse))]
    public async Task<IActionResult> Get([FromRoute]int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetMotorcycleById(id), cancellationToken);
        return result.ToActionResult();
    }

    [HttpDelete("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Remover uma moto")]
    [SwaggerResponse(StatusCodes.Status200OK, "Moto removida com sucesso")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Moto não encontrada", typeof(ApiResponse))]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteMotorcycle(id), cancellationToken);
        return result.ToActionResult();
    }
}