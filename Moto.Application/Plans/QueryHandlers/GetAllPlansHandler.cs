using MediatR;
using Moto.Application.Plans.Queries;
using Moto.Application.Plans.Responses;
using Moto.Domain.Primitives;
using Moto.Domain.Repositories;

namespace Moto.Application.Plans.QueryHandlers;

public sealed class GetAllPlansHandler(
    IPlanRepository _repository) : IRequestHandler<GetAllPlans, Result<List<PlanResponse>>>
{
    public async Task<Result<List<PlanResponse>>> Handle(GetAllPlans request, CancellationToken cancellationToken)
    {
        var plans = await _repository.GetAllAsync(cancellationToken);

        var response = plans.Select(x =>
            new PlanResponse(
                x.Id,
                x.CostPerDay,
                x.Fee)
            ).ToList();

        return Result.Success(response);
    }
}
