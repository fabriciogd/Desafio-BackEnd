using MediatR;
using Moto.Application.Couriers.Commands;
using Moto.Application.Interfaces;
using Moto.Application.Storage;
using Moto.Domain.Errors;
using Moto.Domain.Primitives;
using Moto.Domain.Repositories;

namespace Moto.Application.Couriers.CommandHandlers;

public sealed class UpdateCourierDrivingLicenseImageHandler(
    ICourierRepository _repository,
    IFileStorageService _fileStorageService,
    IUnitOfWork _unitOfWork) : IRequestHandler<UpdateCourierDrivingLicenseImage, Result>
{
    public async Task<Result> Handle(UpdateCourierDrivingLicenseImage request, CancellationToken cancellationToken)
    {
        var courier = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (courier is null)
            return Result.NotFound(DomainErrors.Courier.NotFound);

        if (string.IsNullOrEmpty(request.ImagemCNH))
            return Result.Error(DomainErrors.Courier.RequiredImage);

        var fileBytes = Convert.FromBase64String(request.ImagemCNH);

        var path = await _fileStorageService.UploadAsync(courier.Cnpj, fileBytes);

        courier.UpdateDrivingLicenseImagePath(path);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
