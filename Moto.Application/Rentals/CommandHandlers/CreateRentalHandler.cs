using MediatR;
using Microsoft.Extensions.Logging;
using Moto.Application.Interfaces;
using Moto.Application.Rentals.Commands;
using Moto.Domain.Entities;
using Moto.Domain.Errors;
using Moto.Domain.Primitives;
using Moto.Domain.Repositories;

namespace Moto.Application.Rentals.CommandHandlers;

/// <summary>
/// Handles the creation of a new rental.
/// </summary>
/// <param name="_logger">An instance of <see cref="ILogger{T}"/> for logging information and errors.</param>
/// <param name="_courierRepository">An instance of <see cref="ICourierRepository"/> for data access.</param>
/// <param name="_motorcyleRepository">An instance of <see cref="IMotorcyleRepository"/> for data access.</param>
/// <param name="_planRepository">An instance of <see cref="IPlanRepository"/> for data access.</param>
/// <param name="_rentalRepository">An instance of <see cref="IRentalRepository"/> for data access.</param>
/// <param name="_unitOfWork">An instance of <see cref="IUnitOfWork"/> for managing database transactions.</param>
internal sealed class CreateRentalHandler(
    ILogger<CompleteRentalHandler> _logger,
    ICourierRepository _courierRepository,
    IMotorcyleRepository _motorcyleRepository,
    IPlanRepository _planRepository,
    IRentalRepository _rentalRepository,
    IUnitOfWork _unitOfWork) : IRequestHandler<CreateRental, Result<Rental>>
{
    /// <summary>
    /// Processes the request to create a new rental.
    /// </summary>
    /// <param name="request">The command containing the details of the reantal to be created.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result"/> indicating the outcome of the operation.</returns>
    public async Task<Result<Rental>> Handle(CreateRental request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting create rental with data {@Request}", request);

        var courier = await _courierRepository.GetByIdAsync(request.EntregadorId, cancellationToken);

        if (courier is null)
        {
            _logger.LogError("Courier with {Id} not found", request.EntregadorId);

            return Result<Rental>.NotFound(DomainErrors.Courier.NotFound);
        }

        var plan = await _planRepository.GetByIdAsync(request.Plano, cancellationToken);

        if (plan is null)
        {
            _logger.LogError("Plan with {Id} not found", request.Plano);

            return Result<Rental>.NotFound(DomainErrors.Plan.NotFound);
        }

        var motorcycle = await _motorcyleRepository.GetByIdAsync(request.MotoId, cancellationToken);

        if (motorcycle is null)
        {
            _logger.LogError("Motorcycle with {Id} not found", request.MotoId);

            return Result<Rental>.NotFound(DomainErrors.Motorcycle.NotFound);
        }

        var isMotorcycleRented = await _rentalRepository
            .ExistsRentalToMotorcycleAsync(motorcycle.Id, cancellationToken);

        if (isMotorcycleRented)
        {
            _logger.LogError("Motorcycle with {Id} in use", request.MotoId);

            return Result<Rental>.Error(DomainErrors.Motorcycle.InUse);
        }

        var startDate = DateOnly.FromDateTime(DateTime.Now).AddDays(1);
        var expectedEndDate = startDate.AddDays(plan.Id);

        var rental = Rental.Create(
            courier.Id,
            motorcycle.Id,
            plan.Id,
            startDate,
            expectedEndDate
        );

        if (!rental.IsValid)
        {
            _logger.LogError("Create rental validated with errors {@Errors}", rental.Errors);

            return Result<Rental>.Invalid(rental.Errors);
        }

        await _rentalRepository.AddAsync(rental, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Rental created with success {@Rental}", rental);

        return Result.Created(rental);
    }
}
