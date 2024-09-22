using MediatR;
using Moto.Application.Interfaces;
using Moto.Domain.Exceptions;
using Moto.Domain.Repositories;

namespace Moto.Application.Rentals.CompleteRental;

public sealed class CompleteRentalCommandHandler(
    IRentalRepository _rentalRepository,
    IUnitOfWork _unitOfWork) : IRequestHandler<CompleteRentalCommand, Unit>
{
    public async Task<Unit> Handle(CompleteRentalCommand request, CancellationToken cancellationToken)
    {
        var rental = await _rentalRepository.GetByIdAsync(request.Id, cancellationToken);

        if (rental is null)
            throw new NotFoundException("Locação não encontrada");

        rental.Complete(request.dataDevolucao);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
