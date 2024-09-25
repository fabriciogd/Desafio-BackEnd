using MediatR;
using Microsoft.Extensions.Logging;
using Moto.Application.Contracts.Context;
using Moto.Application.UseCases.Motorcycles.Commands;
using Moto.Domain.Errors;
using Moto.Domain.Primitives;
using Moto.Domain.Repositories;
using Moto.Domain.ValueObjects;

namespace Moto.Application.UseCases.Motorcycles.CommandHandlers;

/// <summary>
/// Handles the update license plate of a motorcycle.
/// </summary>
/// <param name="_logger">An instance of <see cref="ILogger{T}"/> for logging information and errors.</param>
/// <param name="_repository">An instance of <see cref="IMotorcyleRepository"/> for data access.</param>
/// <param name="_unitOfWork">An instance of <see cref="IUnitOfWork"/> for managing database transactions.</param>
public sealed class UpdateLicensePlateHandler(
    ILogger<UpdateLicensePlateHandler> _logger,
    IMotorcyleRepository _repository,
    IUnitOfWork _unitOfWork) : IRequestHandler<UpdateLicensePlate, Result>
{
    /// <summary>
    /// Processes the UpdateLicensePlateHandler request and returns a result indicating the outcome of the update process.
    /// </summary>
    /// <param name="request">The UpdateLicensePlate request containing the ID of the motorcycle to be updated.</param>
    /// <param name="cancellationToken">A token for cancelling the operation.</param>
    /// <returns>
    /// A <see cref="Result"/> indicating the success or failure of the motorcycle deletion process. 
    /// </returns>
    public async Task<Result> Handle(UpdateLicensePlate request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting update license plate with data {@Request}", request);

        var motorcycle = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (motorcycle is null)
        {
            _logger.LogError("Motorcycle with {Id} not found", request.Id);

            return Result.NotFound(DomainErrors.Motorcycle.NotFound);
        }

        var existsWithPlate = await _repository.ExistsByLicensePlateAsync(request.Placa, cancellationToken);

        if (existsWithPlate is true)
        {
            _logger.LogError("License plate already in use by another motorcycle {Plate}", request.Placa);

            return Result.Error(DomainErrors.Motorcycle.AlreadyExists);
        }

        var licensePlate = LicensePlate.Create(request.Placa);

        if (licensePlate.IsValid)
        {
            _logger.LogError("License plate validated with errors {@Errors}", licensePlate.Errors);

            return Result.Invalid(licensePlate.Errors);
        }

        motorcycle.UpdateLicensePlate(licensePlate);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("License plate updated with success {@Motorcycle}", motorcycle);

        return Result.Success();
    }
}