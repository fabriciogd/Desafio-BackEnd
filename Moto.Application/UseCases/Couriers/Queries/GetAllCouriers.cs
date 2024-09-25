using MediatR;
using Moto.Domain.Entities;
using Moto.Domain.Primitives;

namespace Moto.Application.UseCases.Couriers.Queries;

/// <summary>
/// A request to retrieve all couriers from the system.
/// Implements <see cref="IRequest{TResult}"/> to follow the CQRS pattern.
/// </summary>
public sealed record GetAllCouriers() : IRequest<Result<List<Courier>>>;
