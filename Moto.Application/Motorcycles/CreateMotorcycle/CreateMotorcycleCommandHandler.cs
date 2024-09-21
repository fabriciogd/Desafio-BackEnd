using FluentValidation;
using MediatR;
using Moto.Application.Interfaces;
using Moto.Domain.Entities;
using Moto.Domain.Errors;
using Moto.Domain.Repositories;

namespace Moto.Application.Motorcycles.CreateMotorcycle;

public sealed class CreateMotorcycleCommandHandler(
    IValidator<CreateMotorcycleCommand> _validator,
    IMotorcyleRepository _repository,
    IUnitOfWork _unitOfWork) : IRequestHandler<CreateMotorcycleCommand, Unit>
{
    public async Task<Unit> Handle(CreateMotorcycleCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(DomainErrors.Invalid);

        var isPlacaUnique = await _repository.IsPlacaUniqueAsync(request.Placa, cancellationToken);

        if (isPlacaUnique is true)
            throw new ValidationException(DomainErrors.Invalid);

        var motorcycle = Motorcycle.Create(request.Identificador, request.Ano, request.Modelo, request.Placa);

        await _repository.AddAsync(motorcycle, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
