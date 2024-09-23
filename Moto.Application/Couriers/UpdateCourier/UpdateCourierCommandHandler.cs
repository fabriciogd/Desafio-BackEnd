using MediatR;
using Moto.Application.Interfaces;
using Moto.Application.Storage;
using Moto.Domain.Errors;
using Moto.Domain.Exceptions;
using Moto.Domain.Repositories;

namespace Moto.Application.Couriers.UpdateCourier;

public sealed class UpdateCourierCommandHandler(
    ICourierRepository _repository,
    IFileStorageService _fileStorageService,
    IUnitOfWork _unitOfWork) : IRequestHandler<UpdateCourierCommand, Unit>
{
    public async Task<Unit> Handle(UpdateCourierCommand request, CancellationToken cancellationToken)
    {
        var courier = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (courier is null)
            throw new NotFoundException(DomainErrors.Courier.NotFound);

        var fileBytes = Convert.FromBase64String(request.ImagemCNH);

        var path = await _fileStorageService.UploadAsync(courier.Cnpj, fileBytes);

        courier.UpdateDrivingLicenseImagePath(path);

        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}
