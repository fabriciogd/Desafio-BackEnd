using FluentValidation;
using MediatR;
using Moto.Application.Interfaces;
using Moto.Domain.Errors;
using Moto.Domain.Exceptions;
using Moto.Domain.Repositories;

namespace Moto.Application.Motorcycles.UpdateMotorcycle;

public sealed class UpdateMotorcycleCommandHandler (
    IValidator<UpdateMotorcycleCommand> _validator,
    IMotorcyleRepository _repository,
    IUnitOfWork _unitOfWork) : IRequestHandler<UpdateMotorcycleCommand>
{
    public async Task Handle(UpdateMotorcycleCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new InvalidException(DomainErrors.Invalid);

        var motorcycle = await _repository.FindByIdentificatorAsync(request.Id, cancellationToken);

        if (motorcycle is null)
            throw new NotFoundException(DomainErrors.Motorcycle.NotFound);

        if (string.Equals(motorcycle.Placa, request.Placa, StringComparison.InvariantCultureIgnoreCase))
            return;

        var existsWithPlate = await _repository.ExistWithPlateAsync(request.Placa, cancellationToken);

        if (existsWithPlate is true)
            throw new InvalidException(DomainErrors.Invalid);

        motorcycle.UpdatePlate(request.Placa);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}