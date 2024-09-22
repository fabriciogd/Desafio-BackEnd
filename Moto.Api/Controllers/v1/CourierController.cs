using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moto.Api.Models;
using Moto.Application.Couriers.CreateCourier;
using Moto.Application.Couriers.UpdateCourier;
using Moto.Domain.Exceptions;
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
    public async Task<IActionResult> Create([FromBody] CreateCourierCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await mediator.Send(command, cancellationToken);

            return Created();
        }
        catch (ValidationException ex)
        {
            return BadRequest(ApiResponse.WithMessage(ex.Message));
        }
    }

    [HttpPost("{id}/cnh")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Enviar foto da cnh")]
    [SwaggerResponse(StatusCodes.Status201Created, "Foto cadastrada com sucesso")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos", typeof(ApiResponse))]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateCourierCommand command, CancellationToken cancellationToken)
    {
        command = command with { Id = id };

        try
        {
            await mediator.Send(command, cancellationToken);

            return Created();
        }
        catch (ValidationException ex)
        {
            return BadRequest(ApiResponse.WithMessage(ex.Message));
        }
    }
}
