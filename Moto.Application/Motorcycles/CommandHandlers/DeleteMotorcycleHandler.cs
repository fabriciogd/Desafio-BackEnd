using MediatR;
using Moto.Application.Interfaces;
using Moto.Application.Motorcycles.Commands;
using Moto.Domain.Errors;
using Moto.Domain.Primitives;
using Moto.Domain.Repositories;

namespace Moto.Application.Motorcycles.CommandHandlers;

public sealed class DeleteMotorcycleHandler(
    IMotorcyleRepository _repository,
    IRentalRepository _rentalRepository,
    IUnitOfWork _unitOfWork) : IRequestHandler<DeleteMotorcycle, Result>
{
    public async Task<Result> Handle(DeleteMotorcycle request, CancellationToken cancellationToken)
    {
        var motorcycle = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (motorcycle is null)
            return Result.NotFound(DomainErrors.Motorcycle.NotFound);

        var existsRental = await _rentalRepository.ExistsRentalToMotorcycleAsync(request.Id, cancellationToken);

        if (existsRental)
            return Result.Conflict(DomainErrors.Motorcycle.InUse);

        _repository.Remove(motorcycle);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
