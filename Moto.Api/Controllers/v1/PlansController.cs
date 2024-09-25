using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moto.Api.Mappings;
using Moto.Application.UseCases.Plans.Queries;
using Moto.Application.UseCases.Plans.Responses;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Moto.Api.Controllers.v1;

/// <summary>
/// The <see cref="PlansController"/> class is an ASP.NET Core API controller for managing plans.
/// It provides endpoints retrieving plans records.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/planos")]
public class PlansController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Retrieves a list of existing plans.
    /// </summary>
    /// <param name="query">The query to retrieve all plans.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>An IActionResult containing the list of plans.</returns>
    [HttpGet()]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Consultar planos existentes")]
    [SwaggerResponse(StatusCodes.Status200OK, "Lista das planos", typeof(List<PlanResponse>))]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllPlans(), cancellationToken);

        var plans = result.Value.ToResponse();

        return Ok(plans);
    }
}
