using MediatR;
using Microsoft.Extensions.Logging;
using Moto.Application.Motorcycles.Queries;
using Moto.Application.Motorcycles.Responses;
using Moto.Domain.Errors;
using Moto.Domain.Primitives;
using Moto.Domain.Repositories;

namespace Moto.Application.Motorcycles.QueryHandlers;

/// <summary>
/// Handles the query to retrieve a motorcycle by its ID.
/// </summary>
/// <param name="_logger">An instance of <see cref="ILogger{T}"/> for logging information and errors.</param>
/// <param name="_repository">The repository used to access motorcycle data.</param>
public sealed class GetMotorcycleByIdHandler(
    ILogger<GetAllMotrocyclesHandler> _logger,
    IMotorcyleRepository _repository) : IRequestHandler<GetMotorcycleById, Result<MotorcycleResponse>>
{
    /// <summary>
    /// Processes the query to return a motorcycle by its ID.
    /// </summary>
    /// <param name="request">The query containing the motorcycle ID.</param>
    /// <param name="cancellationToken">The token used to propagate notifications that the operation should be canceled.</param>
    /// <returns>A result containing the motorcycle response if found, or a not found result.</returns>
    public async Task<Result<MotorcycleResponse>> Handle(GetMotorcycleById request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting get motorcycle by id {Id}", request.Id);

        var motorcycle = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (motorcycle is null)
        {
            _logger.LogError("Motorcycle with {Id} not found", request.Id);

            return Result<MotorcycleResponse>.NotFound(DomainErrors.Motorcycle.NotFound);
        }

        var response = new MotorcycleResponse(
            motorcycle.Id,
            motorcycle.Year,
            motorcycle.Model,
            motorcycle.LicensePlate.Value
        );

        _logger.LogInformation("Motorcycle founded {Motorcycle}", response);

        return Result.Success(response);
    }
}
