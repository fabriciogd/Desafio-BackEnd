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

/// <summary>
/// The <see cref="MotorcycleController"/> class is an ASP.NET Core API controller for managing motorcycles.
/// It provides endpoints for creating, retrieving, updating, and deleting motorcycle records.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/motos")]
public class MotorcycleController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Creates a new motorcycle record.
    /// </summary>
    /// <param name="command">The command containing the details of the motorcycle to create.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>An IActionResult indicating the result of the operation.</returns>
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

    /// <summary>
    /// Retrieves a list of existing motorcycles.
    /// </summary>
    /// <param name="query">The query to retrieve all motorcycles.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>An IActionResult containing the list of motorcycles.</returns>
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

    /// <summary>
    /// Updates the license plate of an existing motorcycle.
    /// </summary>
    /// <param name="id">The ID of the motorcycle to update.</param>
    /// <param name="command">The command containing the new license plate details.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>An IActionResult indicating the result of the operation.</returns>
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

    /// <summary>
    /// Retrieves the details of an existing motorcycle by its ID.
    /// </summary>
    /// <param name="id">The ID of the motorcycle to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>An IActionResult containing the details of the motorcycle.</returns>
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

    /// <summary>
    /// Deletes a motorcycle record by its ID.
    /// </summary>
    /// <param name="id">The ID of the motorcycle to delete.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>An IActionResult indicating the result of the deletion.</returns>
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