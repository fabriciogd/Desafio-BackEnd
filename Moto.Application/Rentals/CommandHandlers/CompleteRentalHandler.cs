using MediatR;
using Microsoft.Extensions.Logging;
using Moto.Application.Interfaces;
using Moto.Application.Rentals.Commands;
using Moto.Domain.Enums;
using Moto.Domain.Errors;
using Moto.Domain.Primitives;
using Moto.Domain.Repositories;

namespace Moto.Application.Rentals.CommandHandlers;

/// <summary>
/// Handler for processing rental completion requests.
/// This class implements the <see cref="IRequestHandler{TRequest, TResponse}"/> interface 
/// to manage the completion of a rental, updating its status and recording the return date.
/// </summary>
/// <param name="_logger">An instance of <see cref="ILogger{T}"/> for logging information and errors.</param>
/// <param name="_repository">An instance of <see cref="IRentalRepository"/> for data access.</param>
/// <param name="_unitOfWork">An instance of <see cref="IUnitOfWork"/> for managing database transactions.</param>
public sealed class CompleteRentalHandler(
    ILogger<CompleteRentalHandler> _logger,
    IRentalRepository _repository,
    IUnitOfWork _unitOfWork) : IRequestHandler<CompleteRental, Result>
{
    public async Task<Result> Handle(CompleteRental request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting complete rental with data {@Request}", request);

        var rental = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (rental is null)
        {
            _logger.LogError("Rental with {Id} not found", request.Id);

            return Result.NotFound(DomainErrors.Rental.NotFound);
        }

        if (rental.Status == RentStatusEnum.Finished)
        {
            _logger.LogError("Rental already finished with {Id}", request.Id);

            return Result.NotFound(DomainErrors.Rental.StatusFinished);
        }

        rental.Complete(request.dataDevolucao);

        if (!rental.IsValid)
        {
            _logger.LogError("Complete rental validated with errors {@Errors}", rental.Errors);

            return Result.Invalid(rental.Errors);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Rental completed with success {@Rental}", rental);

        return Result.Success();
    }
}
