using MediatR;
using Moto.Application.Interfaces;
using Moto.Application.Storage;
using Moto.Domain.Entities;
using Moto.Domain.Repositories;

namespace Moto.Application.Couriers.CreateCourier;

public sealed class CreateCourierCommandHandler(
    ICourierRepository _repository,
    IFileStorageService _fileStorageService,
    IUnitOfWork _unitOfWork) : IRequestHandler<CreateCourierCommand, Unit>
{
    public async Task<Unit> Handle(CreateCourierCommand request, CancellationToken cancellationToken)
    {
        var courier = Courier.Create(
            request.CNPJ, 
            request.DataNascimento, 
            request.NumeroCNH, 
            request.TipoCNH
        );

        await _repository.AddAsync(courier, cancellationToken);

        var fileBytes = Convert.FromBase64String(request.ImagemCNH);

        var path = await _fileStorageService.UploadAsync(request.CNPJ, fileBytes);

        courier.UpdateDrivingLicenseImagePath(path);

        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}
