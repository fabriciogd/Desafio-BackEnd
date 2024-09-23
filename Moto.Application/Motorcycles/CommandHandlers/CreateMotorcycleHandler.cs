using MediatR;
using Moto.Application.Interfaces;
using Moto.Application.Motorcycles.Commands;
using Moto.Domain.Entities;
using Moto.Domain.Primitives;
using Moto.Domain.Repositories;

namespace Moto.Application.Motorcycles.CommandHandlers;

public sealed class CreateMotorcycleHandler(
    IMotorcyleRepository _repository,
    IUnitOfWork _unitOfWork) : IRequestHandler<CreateMotorcycle, Result>
{
    public async Task<Result> Handle(CreateMotorcycle request, CancellationToken cancellationToken)
    {
        var licensePlateInUse = await _repository.ExistsByLicensePlateAsync(request.Placa, cancellationToken);

        if (licensePlateInUse)
            return Result.Conflict("Placa forneccida já esta em uso por outra moto");

        var motorcycle = Motorcycle.Create(request.Ano, request.Modelo, request.Placa);

        if (!motorcycle.IsValid)
            return Result.Invalid(motorcycle.Errors);

        await _repository.AddAsync(motorcycle, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Created();
    }
}