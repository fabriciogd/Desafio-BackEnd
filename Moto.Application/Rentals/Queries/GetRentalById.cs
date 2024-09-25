using MediatR;
using Moto.Domain.Entities;
using Moto.Domain.Primitives;

namespace Moto.Application.Rentals.Queries;

/// <summary>
/// Represents a request to retrieve the details of a rental transaction by its unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the rental transaction to retrieve.</param>
public sealed record GetRentalById(int Id) : IRequest<Result<Rental>>;