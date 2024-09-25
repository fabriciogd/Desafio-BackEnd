using MediatR;
using Moto.Application.Plans.Queries;
using Moto.Domain.Entities;
using Moto.Domain.Primitives;
using Moto.Domain.Repositories;

namespace Moto.Application.Plans.QueryHandlers;

/// <summary>
/// Handles the query to retrieve a list of plans.
/// </summary>
/// <param name="_repository">The repository used to access plans data.</param>
public sealed class GetAllPlansHandler(
    IPlanRepository _repository) : IRequestHandler<GetAllPlans, Result<List<Plan>>>
{
    public async Task<Result<List<Plan>>> Handle(GetAllPlans request, CancellationToken cancellationToken)
    {
        var plans = await _repository.GetAllAsync(cancellationToken);

        return Result.Success(plans);
    }
}
