using MediatR;
using Microsoft.Extensions.Logging;
using Moto.Application.Motorcycles.Queries;
using Moto.Application.Motorcycles.Responses;
using Moto.Domain.Primitives;
using Moto.Domain.Repositories;

namespace Moto.Application.Motorcycles.QueryHandlers;

/// <summary>
/// Handles the query to retrieve a list of motorcycles, optionally filtered by license plate.
/// </summary>
/// <param name="_logger">An instance of <see cref="ILogger{T}"/> for logging information and errors.</param>
/// <param name="_repository">The repository used to access motorcycle data.</param>
public sealed class GetAllMotrocyclesHandler(
    ILogger<GetAllMotrocyclesHandler> _logger,
    IMotorcyleRepository _repository) : IRequestHandler<GetAllMotrocycles, Result<List<MotorcycleResponse>>>
{
    /// <summary>
    /// Processes the query to return a list of motorcycles based on the provided filter.
    /// </summary>
    /// <param name="request">The query containing the optional license plate filter.</param>
    /// <param name="cancellationToken">The token used to propagate notifications that the operation should be canceled.</param>
    /// <returns>A result containing a list of motorcycle responses if successful.</returns>
    public async Task<Result<List<MotorcycleResponse>>> Handle(GetAllMotrocycles request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting list motorcycle with filters {@Request}", request);

        var motorcycles = await _repository.ListAllAsync(request.Placa, cancellationToken);

        var response = motorcycles.Select(x =>
            new MotorcycleResponse(
                x.Id,
                x.Year,
                x.Model,
                x.LicensePlate.Value)
            ).ToList();

        _logger.LogInformation("Founded {Count} motorcycles", response.Count);

        return Result.Success(response);
    }
}