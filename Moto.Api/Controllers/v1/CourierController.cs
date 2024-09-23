using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moto.Api.Extensions;
using Moto.Api.Models;
using Moto.Application.Couriers.Commands;
using Moto.Application.Couriers.Queries;
using Moto.Application.Couriers.Responses;
using Moto.Application.Motorcycles.Queries;
using Moto.Application.Motorcycles.Responses;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Moto.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/entregadores")]
public class CourierController(IMediator mediator): ControllerBase
{
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Cadastrar entregador")]
    [SwaggerResponse(StatusCodes.Status201Created, "Entregador cadastrado com sucesso")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos", typeof(ApiResponse))]
    public async Task<IActionResult> Create([FromBody] CreateCourier command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result.ToActionResult();
    }

    [HttpGet()]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Consultar entregadores existentes")]
    [SwaggerResponse(StatusCodes.Status200OK, "Lista das entregadores", typeof(ApiResponse<List<CourierResponse>>))]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllCouriers(), cancellationToken);
        return result.ToActionResult();
    }

    [HttpPut("{id}/cnh")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Enviar foto da cnh")]
    [SwaggerResponse(StatusCodes.Status200OK, "Foto atualizada com sucesso")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos", typeof(ApiResponse))]
    public async Task<IActionResult> UpdateCnh([FromRoute] int id, [FromBody] UpdateCourierDrivingLicenseImage command, CancellationToken cancellationToken)
    {
        command = command with { Id = id };

        var result = await mediator.Send(command, cancellationToken);
        return result.ToActionResult();
    }
}
