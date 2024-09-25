using MediatR;
using Microsoft.Extensions.Logging;
using Moto.Application.UseCases.Couriers.Queries;
using Moto.Application.UseCases.Motorcycles.QueryHandlers;
using Moto.Domain.Entities;
using Moto.Domain.Errors;
using Moto.Domain.Primitives;
using Moto.Domain.Repositories;

namespace Moto.Application.UseCases.Couriers.QueryHandlers;

/// <summary>
/// Handles the query to retrieve a courier by its ID.
/// </summary>
/// <param name="_logger">An instance of <see cref="ILogger{T}"/> for logging information and errors.</param>
/// <param name="_repository">The repository used to access courier data.</param>
internal sealed class GetCourierByIdHandler(
    ILogger<GetAllMotrocyclesHandler> _logger,
    ICourierRepository _repository) : IRequestHandler<GetCourierById, Result<Courier>>
{
    /// <summary>
    /// Processes the query to return a courier by its ID.
    /// </summary>
    /// <param name="request">The query containing the courier ID.</param>
    /// <param name="cancellationToken">The token used to propagate notifications that the operation should be canceled.</param>
    /// <returns>A result containing the courier response if found, or a not found result.</returns>
    public async Task<Result<Courier>> Handle(GetCourierById request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting get courier by id {Id}", request.Id);

        var courier = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (courier is null)
        {
            _logger.LogError("Motorcycle with {Id} not found", request.Id);

            return Result<Courier>.NotFound(DomainErrors.Courier.NotFound);
        }

        _logger.LogInformation("Courier founded {Courier}", courier);

        return Result.Success(courier);
    }
}
