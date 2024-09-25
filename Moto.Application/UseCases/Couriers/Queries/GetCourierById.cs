using MediatR;
using Moto.Domain.Entities;
using Moto.Domain.Primitives;

namespace Moto.Application.UseCases.Couriers.Queries;

/// <summary>
/// Query to retrieve a courier by its unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the courier to retrieve.</param>
public sealed  record GetCourierById(int Id) : IRequest<Result<Courier>>;