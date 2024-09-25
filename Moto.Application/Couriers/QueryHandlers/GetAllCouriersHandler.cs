using MediatR;
using Moto.Application.Couriers.Queries;
using Moto.Application.Couriers.Responses;
using Moto.Domain.Entities;
using Moto.Domain.Primitives;
using Moto.Domain.Repositories;

namespace Moto.Application.Couriers.QueryHandlers;

/// <summary>
/// Handles the retrieval of all couriers from the repository.
/// Implements <see cref="IRequestHandler{TRequest, TResult}"/> for the <see cref="GetAllCouriers"/> request.
/// </summary>
/// <param name="_repository">An instance of <see cref="ICourierRepository"/> for data access.</param>
public class GetAllCouriersHandler(
    ICourierRepository _repository) : IRequestHandler<GetAllCouriers, Result<List<Courier>>>
{
    /// <summary>
    /// Handles the request to retrieve all couriers.
    /// </summary>
    /// <param name="request">The request object containing no parameters for retrieving all couriers.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result{T}"/> containing a list of <see cref="CourierResponse"/> objects.</returns>
    public async Task<Result<List<Courier>>> Handle(GetAllCouriers request, CancellationToken cancellationToken)
    {
        var motorcycles = await _repository.GetAllAsync(cancellationToken);

        return Result.Success(motorcycles);
    }
}
