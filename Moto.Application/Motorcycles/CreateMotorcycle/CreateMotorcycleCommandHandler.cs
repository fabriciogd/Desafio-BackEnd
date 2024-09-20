using FluentValidation;
using MediatR;
using Moto.Application.Extensions;
using Moto.Application.Interfaces;
using Moto.Domain.Entities;
using Moto.Domain.Primitives.Result;
using Moto.Domain.Repositories;

namespace Moto.Application.Motorcycles.CreateMotorcycle;

public sealed class CreateMotorcycleCommandHandler(
    IValidator<CreateMotorcycleCommand> _validator,
    IMotorcyleRepository _repository,
    IUnitOfWork _unitOfWork) : IRequestHandler<CreateMotorcycleCommand, Result<CreateMotorcycleResponse>>
{
    public async Task<Result<CreateMotorcycleResponse>> Handle(CreateMotorcycleCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result<CreateMotorcycleResponse>.Invalid(validationResult.AsErrors());

        var motorcycle = new Motorcycle(request.Identificador, request.Ano, request.Modelo, request.Placa);

        await _repository.AddAsync(motorcycle, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CreateMotorcycleResponse>.Success(
            new CreateMotorcycleResponse(
                motorcycle.Id, 
                motorcycle.Identificador,
                motorcycle.Ano, 
                motorcycle.Modelo, 
                motorcycle.Placa)
            );
    }
}
