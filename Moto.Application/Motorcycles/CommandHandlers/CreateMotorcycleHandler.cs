using MediatR;
using Microsoft.Extensions.Logging;
using Moto.Application.Interfaces;
using Moto.Application.Motorcycles.Commands;
using Moto.Application.Motorcycles.Responses;
using Moto.Domain.Entities;
using Moto.Domain.Errors;
using Moto.Domain.Primitives;
using Moto.Domain.Repositories;
using Moto.Domain.ValueObjects;

namespace Moto.Application.Motorcycles.CommandHandlers;

/// <summary>
/// Handles the creation of a new motorcycle.
/// </summary>
/// <param name="_logger">An instance of <see cref="ILogger{T}"/> for logging information and errors.</param>
/// <param name="_repository">An instance of <see cref="IMotorcyleRepository"/> for data access.</param>
/// <param name="_unitOfWork">An instance of <see cref="IUnitOfWork"/> for managing database transactions.</param>
internal sealed class CreateMotorcycleHandler(
    ILogger<CreateMotorcycleHandler> _logger,
    IMotorcyleRepository _repository,
    IUnitOfWork _unitOfWork) : IRequestHandler<CreateMotorcycle, Result>
{
    /// <summary>
    /// Processes the request to create a new motorcycle.
    /// </summary>
    /// <param name="request">The command containing the details of the motorcycle to be created.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result"/> indicating the outcome of the operation.</returns>
    public async Task<Result> Handle(CreateMotorcycle request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting create motorcycle with data {@Request}", request);

        var licensePlateInUse = await _repository.ExistsByLicensePlateAsync(request.Placa, cancellationToken);

        if (licensePlateInUse)
        {
            _logger.LogError("License plate already in use {@Request}", request);

            return Result.Conflict(DomainErrors.Motorcycle.AlreadyExists);
        }

        var licensePlate = LicensePlate.Create(request.Placa);

        var motorcycle = Motorcycle.Create(request.Ano, request.Modelo, licensePlate);

        if (!motorcycle.IsValid)
        {
            _logger.LogError("Create motorcycle validated with errors {@Errors}", motorcycle.Errors);

            return Result.Invalid(motorcycle.Errors);
        }

        await _repository.AddAsync(motorcycle, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Motrocycle created with success {@Motorcycle}", motorcycle);

        var response = new MotorcycleResponse(motorcycle.Id, motorcycle.Year, motorcycle.Model, motorcycle.LicensePlate);

        return Result.Created();
    }
}