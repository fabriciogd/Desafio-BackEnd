using MediatR;
using Moto.Application.Interfaces;
using Moto.Domain.Entities;
using Moto.Domain.Exceptions;
using Moto.Domain.Repositories;

namespace Moto.Application.Rents.CreateRent;

internal sealed class CreateRentalCommandHandler(
    ICourierRepository _courierRepository,
    IMotorcyleRepository _motorcyleRepository,
    IPlanRepository _planRepository,
    IRentalRepository _rentalRepository,
    IUnitOfWork _unitOfWork) : IRequestHandler<CreateRentalCommand, Unit>
{
    public async Task<Unit> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
    {
        var courier = await _courierRepository.GetByIdAsync(request.EntregadorId, cancellationToken);

        if (courier is null)
            throw new NotFoundException("Entregador não encontrado");

        if (courier.DrivingLicenseType is not "A")
            throw new ValidationException("Entregador precisa possuir apenas categoria A");

        var plan = _planRepository.GetByIdAsync(request.Plano, cancellationToken);

        if (plan is null)
            throw new NotFoundException("Plano não encontrado");

        var motorcycle = await _motorcyleRepository.GetByIdAsync(request.MotoId, cancellationToken);

        if (motorcycle is null)
            throw new NotFoundException("Moto não encontrada");

        var isMotorcycleRented = await _rentalRepository
            .ExistsRentalInProgressToMotorcycleAsync(motorcycle.Id, cancellationToken);
    
        if (isMotorcycleRented)
            throw new ValidationException("Moto ja está alugada");

        var rental = Rental.Create(
            courier.Id,
            motorcycle.Id,
            plan.Id,
            request.DataInicio,
            request.DataTermino,
            request.DataPrevisaoTermino
        );

        await _rentalRepository.AddAsync(rental, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
