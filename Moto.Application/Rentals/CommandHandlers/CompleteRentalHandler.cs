using MediatR;
using Moto.Application.Interfaces;
using Moto.Application.Rentals.Commands;
using Moto.Domain.Enums;
using Moto.Domain.Errors;
using Moto.Domain.Primitives;
using Moto.Domain.Repositories;

namespace Moto.Application.Rentals.CommandHandlers;

public sealed class CompleteRentalHandler(
    IRentalRepository _rentalRepository,
    IUnitOfWork _unitOfWork) : IRequestHandler<CompleteRental, Result>
{
    public async Task<Result> Handle(CompleteRental request, CancellationToken cancellationToken)
    {
        var rental = await _rentalRepository.GetByIdAsync(request.Id, cancellationToken);

        if (rental is null)
            return Result.NotFound(DomainErrors.Rental.NotFound);

        if (rental.Status == RentStatusEnum.Finished)
            return Result.NotFound(DomainErrors.Rental.StatusFinished);

        rental.Complete(request.dataDevolucao);

        if (!rental.IsValid)
            return Result.Invalid(rental.Errors);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
