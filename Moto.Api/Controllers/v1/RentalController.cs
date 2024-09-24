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

/// <summary>
/// The <see cref="RentalController"/> class is an ASP.NET Core API controller for managing motorcycle rentals.
/// It provides endpoints for creating a rental, completing a rental, and retrieving rental details.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/locacao")]
public class RentalController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Creates a new motorcycle rental.
    /// </summary>
    /// <param name="command">The command containing the details of the rental to create.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>An IActionResult indicating the result of the operation.</returns>
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

    /// <summary>
    /// Completes a rental by informing the return date and calculating the total cost.
    /// </summary>
    /// <param name="id">The ID of the rental to complete.</param>
    /// <param name="command">The command containing the return date and other details.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>An IActionResult indicating the result of the operation.</returns>
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

    /// <summary>
    /// Retrieves the details of a rental by its ID.
    /// </summary>
    /// <param name="command">The command containing the ID of the rental to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>An IActionResult containing the details of the rental.</returns>
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