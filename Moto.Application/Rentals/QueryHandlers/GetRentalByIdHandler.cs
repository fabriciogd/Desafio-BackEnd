using MediatR;
using Moto.Application.Rentals.Queries;
using Moto.Application.Rents.Responses;
using Moto.Domain.Errors;
using Moto.Domain.Primitives;
using Moto.Domain.Repositories;

namespace Moto.Application.Rentals.QueryHandlers;

public sealed class GetRentalByIdHandler(
    IRentalRepository _rentalRepository) : IRequestHandler<GetRentalById, Result<RentalResponse>>
{
    public async Task<Result<RentalResponse>> Handle(GetRentalById request, CancellationToken cancellationToken)
    {
        var rental = await _rentalRepository.GetByIdAsync(request.Id, cancellationToken);

        if (rental is null)
            return Result<RentalResponse>.NotFound(DomainErrors.Rental.NotFound);

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

        return Result.Success(response);
    }
}