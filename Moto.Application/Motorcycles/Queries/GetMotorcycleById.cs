using MediatR;
using Moto.Application.Motorcycles.Responses;
using Moto.Domain.Primitives;

namespace Moto.Application.Motorcycles.Queries;

/// <summary>
/// Query to retrieve a motorcycle by its unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the motorcycle to retrieve.</param>
public sealed record GetMotorcycleById(int Id) : IRequest<Result<MotorcycleResponse>>;
