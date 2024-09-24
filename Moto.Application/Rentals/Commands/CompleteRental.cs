using MediatR;
using Moto.Domain.Primitives;

namespace Moto.Application.Rentals.Commands;

/// <summary>
/// Represents a request to complete a rental transaction.
/// Contains the rental ID and the return date of the rented motorcycle.
/// </summary>
/// <param name="Id">The unique identifier of the rental to be completed.</param>
/// <param name="dataDevolucao">The date when the rented motorcycle is returned.</param>
public sealed record CompleteRental(int Id, DateTime dataDevolucao) : IRequest<Result>;
