﻿using MediatR;
using Microsoft.Extensions.Logging;
using Moto.Application.Contracts.Context;
using Moto.Application.Contracts.Storage;
using Moto.Application.File;
using Moto.Application.UseCases.Couriers.Commands;
using Moto.Domain.Entities;
using Moto.Domain.Errors;
using Moto.Domain.Primitives;
using Moto.Domain.Repositories;

namespace Moto.Application.UseCases.Couriers.CommandHandlers;

/// <summary>
/// Handles the creation of a new courier by processing the <see cref="CreateCourier"/> command.
/// Implements <see cref="IRequestHandler{TRequest, TResult}"/> to manage request handling in a CQRS pattern.
/// </summary>
/// <param name="_logger">An instance of <see cref="ILogger{T}"/> for logging information and errors.</param>
/// <param name="_courierRepository">An instance of <see cref="ICourierRepository"/> for data access.</param>
/// <param name="_fileExtensionChecker">An instance of <see cref="IFileExtensionChecker"/> for validating file extensions.</param>
/// <param name="_fileStorageService">An instance of <see cref="IFileStorageService"/> for handling file uploads.</param>
/// <param name="_unitOfWork">An instance of <see cref="IUnitOfWork"/> for managing database transactions.</param>
public sealed class CreateCourierHandler(
    ILogger<CreateCourierHandler> _logger,
    ICourierRepository _courierRepository,
    IFileExtensionChecker _fileExtensionChecker,
    IFileStorageService _fileStorageService,
    IUnitOfWork _unitOfWork) : IRequestHandler<CreateCourier, Result<Courier>>
{
    /// <summary>
    /// Handles the creation of a new courier.
    /// </summary>
    /// <param name="request">The command containing the courier's data.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result"/> indicating the outcome of the operation.</returns>
    public async Task<Result<Courier>> Handle(CreateCourier request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting create courier with data {@Request}", request);

        var courier = Courier.Create(
            request.Cnpj,
            request.DataNascimento,
            request.NumeroCnh,
            request.TipoCnh
        );

        if (!courier.IsValid)
        {
            _logger.LogError("Create courier validated with errors {@Errors}", courier.Errors);

            return Result<Courier>.Invalid(courier.Errors);
        }

        if (await _courierRepository.ExistsByCnpjAsync(courier.Cnpj, cancellationToken))
        {
            _logger.LogError("Cnpj {Cnpj} already in use", courier.Cnpj);

            return Result<Courier>.Error(DomainErrors.Courier.CnpjDuplicated);
        }

        if (await _courierRepository.ExistsByDrivingLicenseAsync(courier.DrivingLicense, cancellationToken))
        {
            _logger.LogError("Driving license {DrivingLicense} already in use", courier.DrivingLicense);

            return Result<Courier>.Error(DomainErrors.Courier.DrivingLicenseDuplicated);
        }

        await _courierRepository.AddAsync(courier, cancellationToken);

        if (!string.IsNullOrEmpty(request.ImagemCnh))
        {
            var (isValid, extension) = _fileExtensionChecker.Validate(request.ImagemCnh, "png", "bmp");

            if (!isValid)
            {
                _logger.LogError("Driving license image invalid");

                return Result<Courier>.Error(DomainErrors.Courier.IncorretImageFormat);
            }

            var path = await _fileStorageService.UploadAsync(request.Cnpj, extension, request.ImagemCnh);

            courier.UpdateDrivingLicenseImagePath(path);
        }

        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Courier created with success {@Courier}", courier);

        return Result.Created(courier);
    }
}
