using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moto.Api.Extensions;
using Moto.Api.Models;
using Moto.Application.Rentals.Commands;
using Moto.Application.Rentals.Queries;
using Moto.Application.Rents.Responses;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Moto.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/locacao")]
public class RentalController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Alugar uma moto")]
    [SwaggerResponse(StatusCodes.Status201Created, "Locação efetuada com sucesso")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos", typeof(ApiResponse))]
    public async Task<IActionResult> Create([FromBody] CreateRental command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result.ToActionResult();
    }

    [HttpPut("{id}/devolucao")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Informar data de devolução e calcular valor")]
    [SwaggerResponse(StatusCodes.Status200OK, "Data de devolução informada com sucesso", typeof(ApiResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Locação não encontrada", typeof(ApiResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos", typeof(ApiResponse))]
    public async Task<IActionResult> Complete([FromRoute] int id, [FromBody] CompleteRental command, CancellationToken cancellationToken)
    {
        command = command with { Id = id };

        var result = await mediator.Send(command, cancellationToken);
        return result.ToActionResult();
    }

    [HttpGet("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Consultar moto existente por id")]
    [SwaggerResponse(StatusCodes.Status200OK, "Detalhes da locação", typeof(ApiResponse<RentalResponse>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Dados não encontrado", typeof(ApiResponse))]
    public async Task<IActionResult> Get([FromRoute] GetRentalById command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result.ToActionResult();
    }
}