using MediatR;
using Moto.Domain.Primitives;

namespace Moto.Application.Motorcycles.Commands;

/// <summary>
/// Represents a command to delete a motorcycle by its ID.
/// </summary>
/// <param name="Id">The unique identifier of the motorcycle to be deleted.</param>
public sealed record DeleteMotorcycle(int Id) : IRequest<Result>;
