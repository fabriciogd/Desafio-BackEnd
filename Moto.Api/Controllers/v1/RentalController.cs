using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moto.Api.Models;
using Moto.Application.Rentals.CompleteRental;
using Moto.Application.Rents.CreateRent;
using Moto.Application.Rents.GetRental;
using Moto.Application.Rents.Response;
using Moto.Domain.Exceptions;
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
    public async Task<IActionResult> Create([FromBody] CreateRentalCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await mediator.Send(command, cancellationToken);

            return Created();
        }
        catch (Exception ex) when (ex is NotFoundException || ex is ValidationException)
        {
            return BadRequest(ApiResponse.WithMessage("Dados inválidos"));
        }
    }

    [HttpGet("/{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Consultar moto existente por id")]
    [SwaggerResponse(StatusCodes.Status200OK, "Detalhes da locação", typeof(RentalResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Dados não encontrado", typeof(ApiResponse))]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var command = new GetRentalCommand(id);

        try
        {
            var response = await mediator.Send(command, cancellationToken);

            return Ok(response);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ApiResponse.WithMessage(ex.Message));
        }
    }

    [HttpGet("/{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Informar data de devolução e calcular valor")]
    [SwaggerResponse(StatusCodes.Status200OK, "Data de devolução informada com sucesso", typeof(RentalResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Locação não encontrada", typeof(ApiResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos", typeof(ApiResponse))]
    public async Task<IActionResult> Complete([FromRoute] int id, [FromBody] CompleteRentalCommand command, CancellationToken cancellationToken)
    {
        command = command with { Id = id };

        try
        {
            var response = await mediator.Send(command, cancellationToken);

            return Ok(response);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ApiResponse.WithMessage(ex.Message));
        }
        catch(ValidationException ex)
        {
            return BadRequest(ApiResponse.WithMessage("Dados inválidos"));
        }
    }
}