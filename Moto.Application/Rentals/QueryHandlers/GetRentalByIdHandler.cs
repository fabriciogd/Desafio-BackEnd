using MediatR;
using Microsoft.Extensions.Logging;
using Moto.Application.Rentals.Queries;
using Moto.Application.Rents.Responses;
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
    IRentalRepository _repository) : IRequestHandler<GetRentalById, Result<RentalResponse>>
{
    public async Task<Result<RentalResponse>> Handle(GetRentalById request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting get rental by id {Id}", request.Id);

        var rental = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (rental is null)
        {
            _logger.LogError("Rental with {Id} not found", request.Id);

            return Result<RentalResponse>.NotFound(DomainErrors.Rental.NotFound);
        }

        var response = new RentalResponse(
            rental.Id,
            rental.CourierId,
            rental.MotorcycleId,
            rental.PlanId,
            rental.StartDate,
            rental.EndDate,
            rental.ExpectedEndDate,
            rental.TotalPayment
        );

        _logger.LogInformation("Rental founded {Rental}", response);

        return Result.Success(response);
    }
}