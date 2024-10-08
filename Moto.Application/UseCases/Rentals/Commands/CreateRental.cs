﻿using MediatR;
using Moto.Domain.Entities;
using Moto.Domain.Primitives;

namespace Moto.Application.UseCases.Rentals.Commands;

/// <summary>
/// Represents a request to create a new rental transaction.
/// Contains the necessary information to initiate the rental process for a motorcycle.
/// </summary>
/// <param name="EntregadorId">The unique identifier of the courier initiating the rental.</param>
/// <param name="MotoId">The unique identifier of the motorcycle being rented.</param>
/// <param name="Plano">The unique identifier of the rental plan selected.</param>
public sealed record CreateRental(
    int EntregadorId,
    int MotoId,
    int Plano
) : IRequest<Result<Rental>>;