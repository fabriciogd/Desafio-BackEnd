using MediatR;
using Moto.Domain.Primitives;

namespace Moto.Application.Rentals.Commands;

/// <summary>
/// Represents a request to create a new rental transaction.
/// Contains the necessary information to initiate the rental process for a motorcycle.
/// </summary>
/// <param name="EntregadorId">The unique identifier of the courier initiating the rental.</param>
/// <param name="MotoId">The unique identifier of the motorcycle being rented.</param>
/// <param name="Plano">The unique identifier of the rental plan selected.</param>
/// <param name="DataPrevisaoTermino">The expected return date for the rented motorcycle.</param>
public sealed record CreateRental(
    int EntregadorId,
    int MotoId,
    int Plano,
    DateTime DataPrevisaoTermino
) : IRequest<Result>;