using MediatR;
using Moto.Application.Rents.Response;
using Moto.Domain.Exceptions;
using Moto.Domain.Repositories;

namespace Moto.Application.Rents.GetRental;

public sealed class GetRentalCommandHandler(
    IRentalRepository _rentalRepository) : IRequestHandler<GetRentalCommand, RentalResponse>
{
    public async Task<RentalResponse> Handle(GetRentalCommand request, CancellationToken cancellationToken)
    {
        var rental = await _rentalRepository.GetByIdAsync(request.Id, cancellationToken);

        if (rental is null)
            throw new NotFoundException("Locação não encontrada");

        return new RentalResponse(
            rental.CourierId,
            rental.MotorcycleId,
            rental.StartDate,
            rental.EndDate,
            rental.ExpectedEndDate
        );
    }
}