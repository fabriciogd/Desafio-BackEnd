﻿using MediatR;
using Microsoft.Extensions.Logging;
using Moto.Application.Contracts.Context;
using Moto.Application.UseCases.Motorcycles.Commands;
using Moto.Domain.Entities;
using Moto.Domain.Errors;
using Moto.Domain.Primitives;
using Moto.Domain.Repositories;
using Moto.Domain.ValueObjects;

namespace Moto.Application.UseCases.Motorcycles.CommandHandlers;

/// <summary>
/// Handles the creation of a new motorcycle.
/// </summary>
/// <param name="_logger">An instance of <see cref="ILogger{T}"/> for logging information and errors.</param>
/// <param name="_repository">An instance of <see cref="IMotorcyleRepository"/> for data access.</param>
/// <param name="_unitOfWork">An instance of <see cref="IUnitOfWork"/> for managing database transactions.</param>
internal sealed class CreateMotorcycleHandler(
    ILogger<CreateMotorcycleHandler> _logger,
    IMotorcyleRepository _repository,
    IUnitOfWork _unitOfWork) : IRequestHandler<CreateMotorcycle, Result<Motorcycle>>
{
    /// <summary>
    /// Processes the request to create a new motorcycle.
    /// </summary>
    /// <param name="request">The command containing the details of the motorcycle to be created.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result"/> indicating the outcome of the operation.</returns>
    public async Task<Result<Motorcycle>> Handle(CreateMotorcycle request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting create motorcycle with data {@Request}", request);

        if (await _repository.ExistsByLicensePlateAsync(request.Placa, cancellationToken))
        {
            _logger.LogError("License plate already in use {@Request}", request);

            return Result<Motorcycle>.Error(DomainErrors.Motorcycle.AlreadyExists);
        }

        var licensePlate = LicensePlate.Create(request.Placa);

        var motorcycle = Motorcycle.Create(request.Ano, request.Modelo, licensePlate);

        if (!motorcycle.IsValid)
        {
            _logger.LogError("Create motorcycle validated with errors {@Errors}", motorcycle.Errors);

            return Result<Motorcycle>.Invalid(motorcycle.Errors);
        }

        await _repository.AddAsync(motorcycle, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Motrocycle created with success {@Motorcycle}", motorcycle);

        return Result.Created(motorcycle);
    }
}