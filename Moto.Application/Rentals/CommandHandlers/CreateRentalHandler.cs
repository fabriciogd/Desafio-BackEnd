using MediatR;
using Moto.Application.Interfaces;
using Moto.Application.Rentals.Commands;
using Moto.Domain.Entities;
using Moto.Domain.Errors;
using Moto.Domain.Primitives;
using Moto.Domain.Repositories;

namespace Moto.Application.Rentals.CommandHandlers;

internal sealed class CreateRentalHandler(
    ICourierRepository _courierRepository,
    IMotorcyleRepository _motorcyleRepository,
    IPlanRepository _planRepository,
    IRentalRepository _rentalRepository,
    IUnitOfWork _unitOfWork) : IRequestHandler<CreateRental, Result>
{
    public async Task<Result> Handle(CreateRental request, CancellationToken cancellationToken)
    {
        var courier = await _courierRepository.GetByIdAsync(request.EntregadorId, cancellationToken);

        if (courier is null)
            return Result.NotFound(DomainErrors.Courier.NotFound);

        var plan = _planRepository.GetByIdAsync(request.Plano, cancellationToken);

        if (plan is null)
            return Result.NotFound(DomainErrors.Plan.NotFound);

        var motorcycle = await _motorcyleRepository.GetByIdAsync(request.MotoId, cancellationToken);

        if (motorcycle is null)
            return Result.NotFound(DomainErrors.Motorcycle.NotFound);

        var isMotorcycleRented = await _rentalRepository
            .ExistsRentalToMotorcycleAsync(motorcycle.Id, cancellationToken);

        if (isMotorcycleRented)
            return Result.Error(DomainErrors.Motorcycle.NotFound);

        var rental = Rental.Create(
            courier.Id,
            motorcycle.Id,
            plan.Id,
            request.DataInicio,
            request.DataTermino,
            request.DataPrevisaoTermino
        );

        if (!rental.IsValid)
            return Result.Invalid(rental.Errors);

        await _rentalRepository.AddAsync(rental, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Created();
    }
}
