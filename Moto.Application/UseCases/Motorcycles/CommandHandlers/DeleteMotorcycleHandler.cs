using MediatR;
using Microsoft.Extensions.Logging;
using Moto.Application.Contracts.Context;
using Moto.Application.UseCases.Motorcycles.Commands;
using Moto.Domain.Errors;
using Moto.Domain.Primitives;
using Moto.Domain.Repositories;

namespace Moto.Application.UseCases.Motorcycles.CommandHandlers;

/// <summary>
/// Handles the deletion of a motorcycle.
/// </summary>
/// <param name="_logger">An instance of <see cref="ILogger{T}"/> for logging information and errors.</param>
/// <param name="_repository">An instance of <see cref="IMotorcyleRepository"/> for data access.</param>
/// <param name="_rentalRepository">An instance of <see cref="IRentalRepository"/> for data access.</param>
/// <param name="_unitOfWork">An instance of <see cref="IUnitOfWork"/> for managing database transactions.</param>
public sealed class DeleteMotorcycleHandler(
    ILogger<DeleteMotorcycleHandler> _logger,
    IMotorcyleRepository _repository,
    IRentalRepository _rentalRepository,
    IUnitOfWork _unitOfWork) : IRequestHandler<DeleteMotorcycle, Result>
{

    /// <summary>
    /// Processes the DeleteMotorcycle request and returns a result indicating the outcome of the deletion process.
    /// </summary>
    /// <param name="request">The DeleteMotorcycle request containing the ID of the motorcycle to be deleted.</param>
    /// <param name="cancellationToken">A token for cancelling the operation.</param>
    /// <returns>
    /// A <see cref="Result"/> indicating the success or failure of the motorcycle deletion process. 
    /// </returns>
    public async Task<Result> Handle(DeleteMotorcycle request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting delete motorcycle with data {@Request}", request);

        var motorcycle = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (motorcycle is null)
        {
            _logger.LogError("Motorcycle with {Id} not found", request.Id);

            return Result.NotFound(DomainErrors.Motorcycle.NotFound);
        }

        var existsRental = await _rentalRepository.ExistsRentalToMotorcycleAsync(request.Id, cancellationToken);

        if (existsRental)
        {
            _logger.LogError("Motorcycle with {Id} already in use", request.Id);

            return Result.Error(DomainErrors.Motorcycle.InUse);
        }

        _repository.Remove(motorcycle);

        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Motrocycle deleted with success with {Id}", motorcycle.Id);

        return Result.Success();
    }
}
