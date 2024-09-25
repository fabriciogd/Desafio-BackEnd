using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moto.Api.Extensions;
using Moto.Api.Mappings;
using Moto.Api.Models;
using Moto.Application.UseCases.Couriers.Commands;
using Moto.Application.UseCases.Couriers.Queries;
using Moto.Application.UseCases.Couriers.Responses;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Moto.Api.Controllers.v1;

/// <summary>
/// The <see cref="CourierController"/> class is an ASP.NET Core API controller for managing couriers.
/// It provides endpoints for creating, retrieving, and updating courier records.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/entregadores")]
public class CourierController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Creates a new courier record.
    /// </summary>
    /// <param name="command">The command containing the details of the courier to create.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>An IActionResult indicating the result of the operation.</returns>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Cadastrar entregador")]
    [SwaggerResponse(StatusCodes.Status200OK, "Entregador cadastrado com sucesso", typeof(CourierResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos", typeof(ApiResponse))]
    public async Task<IActionResult> Create([FromBody] CreateCourier command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return result.ToHttpNonSuccessResult();

        var courier = result.Value.ToResponse();

        return Ok(courier);
    }

    /// <summary>
    /// Retrieves a list of existing couriers.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>An IActionResult containing the list of couriers.</returns>
    [HttpGet()]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Consultar entregadores existentes")]
    [SwaggerResponse(StatusCodes.Status200OK, "Lista das entregadores", typeof(List<CourierResponse>))]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllCouriers(), cancellationToken);

        var motorcycles = result.Value.ToResponse();

        return Ok(motorcycles);
    }

    /// <summary>
    /// Updates the driving license image for an existing courier.
    /// </summary>
    /// <param name="id">The ID of the courier to update.</param>
    /// <param name="command">The command containing the new driving license image details.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>An IActionResult indicating the result of the operation.</returns>
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

        if (!result.IsSuccess)
            return result.ToHttpNonSuccessResult();

        return Ok();
    }
}