using MediatR;
using Moto.Application.Plans.Responses;
using Moto.Domain.Primitives;

namespace Moto.Application.Plans.Queries;

/// <summary>
/// A request to retrieve all plans from the system.
/// Implements <see cref="IRequest{TResult}"/> to follow the CQRS pattern.
/// </summary>
public sealed record GetAllPlans() : IRequest<Result<List<PlanResponse>>>;
