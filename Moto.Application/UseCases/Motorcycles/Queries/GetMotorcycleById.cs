﻿using MediatR;
using Moto.Domain.Entities;
using Moto.Domain.Primitives;

namespace Moto.Application.UseCases.Motorcycles.Queries;

/// <summary>
/// Query to retrieve a motorcycle by its unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the motorcycle to retrieve.</param>
public sealed record GetMotorcycleById(int Id) : IRequest<Result<Motorcycle>>;
