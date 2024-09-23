using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moto.Api.Models;
using Moto.Application.Motorcycles.CreateMotorcycle;
using Moto.Application.Motorcycles.DeleteMotorcycle;
using Moto.Application.Motorcycles.GetMotorcycle;
using Moto.Application.Motorcycles.ListMotorcycle;
using Moto.Application.Motorcycles.Response;
using Moto.Application.Motorcycles.UpdateMotorcycle;
using Moto.Domain.Exceptions;
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
    public async Task<IActionResult> Create([FromBody] CreateMotorcycleCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await mediator.Send(command, cancellationToken);

            return StatusCode(StatusCodes.Status201Created);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ApiResponse.WithMessage(ex.Message));
        }
    }

    [HttpGet()]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Consultar motos existentes")]
    [SwaggerResponse(StatusCodes.Status200OK, "Lista das motos", typeof(IReadOnlyCollection<MotorcycleResponse>))]
    public async Task<IActionResult> List([FromQuery] ListMotorcycleQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpPut("{id}/placa")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Modificar a placa de uma moto")]
    [SwaggerResponse(StatusCodes.Status200OK, "Placa modificada com sucesso", typeof(ApiResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos", typeof(ApiResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Moto não encontrada", typeof(ApiResponse))]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateMotorcycleCommand command, CancellationToken cancellationToken)
    {
        command = command with { Id = id };

        try
        {
            await mediator.Send(command, cancellationToken);

            return Ok(ApiResponse.WithMessage("Placa motificada com sucesso"));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ApiResponse.WithMessage(ex.Message));
        }
        catch (ValidationException ex)
        {
            return BadRequest(ApiResponse.WithMessage(ex.Message));
        }
    }

    [HttpGet("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Consultar moto existente por id")]
    [SwaggerResponse(StatusCodes.Status200OK, "Detalhes da moto", typeof(MotorcycleResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Moto não encontrada", typeof(ApiResponse))]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var command = new GetMotorcycleCommand(id);

        try
        {
            var response = await mediator.Send(command, cancellationToken);

            return Ok(response);
        }
        catch(NotFoundException ex)
        {
            return NotFound(ApiResponse.WithMessage(ex.Message));
        }
    }

    [HttpDelete("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Remover uma moto")]
    [SwaggerResponse(StatusCodes.Status200OK, "Detalhes da moto")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Moto não encontrada", typeof(ApiResponse))]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var command = new DeleteMotorcycleCommand(id);

        try
        {
            await mediator.Send(command, cancellationToken);

            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ApiResponse.WithMessage(ex.Message));
        }
        catch (ValidationException ex)
        {
            return BadRequest(ApiResponse.WithMessage(ex.Message));
        }
    }
}