using MediatR;
using Moto.Application.Couriers.Commands;
using Moto.Application.Interfaces;
using Moto.Application.Storage;
using Moto.Domain.Entities;
using Moto.Domain.Primitives;
using Moto.Domain.Repositories;

namespace Moto.Application.Couriers.CommandHandlers;

public sealed class CreateCourierHandler(
    ICourierRepository _repository,
    IFileStorageService _fileStorageService,
    IUnitOfWork _unitOfWork) : IRequestHandler<CreateCourier, Result>
{
    public async Task<Result> Handle(CreateCourier request, CancellationToken cancellationToken)
    {
        var courier = Courier.Create(
            request.CNPJ,
            request.DataNascimento,
            request.NumeroCNH,
            request.TipoCNH
        );

        if (!courier.IsValid)
            return Result.Invalid(courier.Errors);

        await _repository.AddAsync(courier, cancellationToken);

        if (!string.IsNullOrEmpty(request.ImagemCNH))
        {
            var fileBytes = Convert.FromBase64String(request.ImagemCNH);

            var path = await _fileStorageService.UploadAsync(request.CNPJ, fileBytes);

            courier.UpdateDrivingLicenseImagePath(path);
        }

        await _unitOfWork.SaveChangesAsync();

        return Result.Created();
    }
}
