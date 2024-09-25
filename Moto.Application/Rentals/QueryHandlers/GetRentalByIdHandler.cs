﻿using MediatR;
using Microsoft.Extensions.Logging;
using Moto.Application.Rentals.Queries;
using Moto.Domain.Entities;
using Moto.Domain.Errors;
using Moto.Domain.Primitives;
using Moto.Domain.Repositories;

namespace Moto.Application.Rentals.QueryHandlers;

/// <summary>
/// Handles the retrieval of a rental transaction by its unique identifier.
/// Implements the <see cref="IRequestHandler{TRequest,TResponse}"/> interface.
/// </summary>
/// <param name="_logger">An instance of <see cref="ILogger{T}"/> for logging information and errors.</param>
/// <param name="_repository">An instance of <see cref="IRentalRepository"/> for data access.</param>
public sealed class GetRentalByIdHandler(
    ILogger<GetRentalByIdHandler> _logger,
    IRentalRepository _repository) : IRequestHandler<GetRentalById, Result<Rental>>
{
    public async Task<Result<Rental>> Handle(GetRentalById request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting get rental by id {Id}", request.Id);

        var rental = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (rental is null)
        {
            _logger.LogError("Rental with {Id} not found", request.Id);

            return Result<Rental>.NotFound(DomainErrors.Rental.NotFound);
        }

        _logger.LogInformation("Rental founded {Rental}", rental);

        return Result.Success(rental);
    }
}