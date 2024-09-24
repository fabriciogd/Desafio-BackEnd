using MediatR;
using Microsoft.Extensions.Logging;
using Moto.Application.Couriers.Commands;
using Moto.Application.File;
using Moto.Application.Interfaces;
using Moto.Application.Storage;
using Moto.Domain.Errors;
using Moto.Domain.Primitives;
using Moto.Domain.Repositories;

namespace Moto.Application.Couriers.CommandHandlers;

/// <summary>
/// Handles the updating of a courier's driver's license image by processing the <see cref="UpdateCourierDrivingLicenseImage"/> command.
/// Implements <see cref="IRequestHandler{TRequest, TResult}"/> to manage request handling in a CQRS pattern.
/// </summary>
/// <param name="_logger">An instance of <see cref="ILogger{T}"/> for logging information and errors.</param>
/// <param name="_repository">An instance of <see cref="ICourierRepository"/> for data access.</param>
/// <param name="_fileExtensionChecker">An instance of <see cref="IFileExtensionChecker"/> for validating file extensions.</param>
/// <param name="_fileStorageService">An instance of <see cref="IFileStorageService"/> for handling file uploads.</param>
/// <param name="_unitOfWork">An instance of <see cref="IUnitOfWork"/> for managing database transactions.</param>
public sealed class UpdateCourierDrivingLicenseImageHandler(
    ILogger<UpdateCourierDrivingLicenseImageHandler> _logger,
    ICourierRepository _repository,
    IFileExtensionChecker _fileExtensionChecker,
    IFileStorageService _fileStorageService,
    IUnitOfWork _unitOfWork) : IRequestHandler<UpdateCourierDrivingLicenseImage, Result>
{
    /// <summary>
    /// Handles the updating of the driver's license image for a courier.
    /// </summary>
    /// <param name="request">The command containing the courier's ID and the new driver's license image.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result"/> indicating the outcome of the operation.</returns>
    public async Task<Result> Handle(UpdateCourierDrivingLicenseImage request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting update driving license image with data {@Request}", request);

        var courier = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (courier is null)
        {
            _logger.LogError("Courier with {Id} not found", request.Id);

            return Result.NotFound(DomainErrors.Courier.NotFound);
        }

        if (string.IsNullOrEmpty(request.ImagemCNH))
        {
            _logger.LogError("Driving license image cannot be null");

            return Result.Error(DomainErrors.Courier.RequiredImage);
        }

        var (isValid, extension) = _fileExtensionChecker.Validate(request.ImagemCNH, "png", "bmp");

        if (isValid)
        {
            _logger.LogError("Driving license image invalid");

            return Result.Error(DomainErrors.Courier.IncorretImageFormat);
        }

        var path = await _fileStorageService.UploadAsync(courier.Cnpj, extension, request.ImagemCNH);

        courier.UpdateDrivingLicenseImagePath(path);

        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Driving license image updated with success {@Courier}", courier);

        return Result.Success();
    }
}
