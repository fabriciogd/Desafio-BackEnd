using MediatR;
using Microsoft.Extensions.Logging;
using Moto.Application.UseCases.Motorcycles.Queries;
using Moto.Domain.Entities;
using Moto.Domain.Primitives;
using Moto.Domain.Repositories;

namespace Moto.Application.UseCases.Motorcycles.QueryHandlers;

/// <summary>
/// Handles the query to retrieve a list of motorcycles, optionally filtered by license plate.
/// </summary>
/// <param name="_logger">An instance of <see cref="ILogger{T}"/> for logging information and errors.</param>
/// <param name="_repository">The repository used to access motorcycle data.</param>
public sealed class GetAllMotrocyclesHandler(
    ILogger<GetAllMotrocyclesHandler> _logger,
    IMotorcyleRepository _repository) : IRequestHandler<GetAllMotrocycles, Result<List<Motorcycle>>>
{
    /// <summary>
    /// Processes the query to return a list of motorcycles based on the provided filter.
    /// </summary>
    /// <param name="request">The query containing the optional license plate filter.</param>
    /// <param name="cancellationToken">The token used to propagate notifications that the operation should be canceled.</param>
    /// <returns>A result containing a list of motorcycle responses if successful.</returns>
    public async Task<Result<List<Motorcycle>>> Handle(GetAllMotrocycles request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting list motorcycle with filters {@Request}", request);

        var motorcycles = await _repository.ListAllAsync(request.Placa, cancellationToken);

        _logger.LogInformation("Founded {Count} motorcycles", motorcycles.Count);

        return Result.Success(motorcycles);
    }
}