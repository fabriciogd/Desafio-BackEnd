using Moto.Application.UseCases.Plans.Responses;
using Moto.Domain.Entities;

namespace Moto.Api.Mappings;
public static class PlanMapping
{
    public static PlanResponse ToResponse(this Plan plan)
        => new PlanResponse(plan.Id, plan.CostPerDay, plan.Fee);

    public static List<PlanResponse> ToResponse(this List<Plan> list)
        => list.Select(o => o.ToResponse()).ToList();
}
