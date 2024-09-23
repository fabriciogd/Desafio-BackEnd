using MediatR;
using Moto.Application.Interfaces;
using Moto.Application.Motorcycles.Commands;
using Moto.Domain.Errors;
using Moto.Domain.Primitives;
using Moto.Domain.Repositories;

namespace Moto.Application.Motorcycles.CommandHandlers;

public sealed class UpdateLicensePlateHandler(
    IMotorcyleRepository _repository,
    IUnitOfWork _unitOfWork) : IRequestHandler<UpdateLicensePlate, Result>
{
    public async Task<Result> Handle(UpdateLicensePlate request, CancellationToken cancellationToken)
    {
        var motorcycle = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (motorcycle is null)
            return Result.NotFound(DomainErrors.Motorcycle.NotFound);

        var existsWithPlate = await _repository.ExistsByLicensePlateAsync(request.Placa, cancellationToken);

        if (existsWithPlate is true)
            return Result.Conflict(DomainErrors.Motorcycle.AlreadyExists);

        motorcycle.UpdateLicensePlate(request.Placa);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}