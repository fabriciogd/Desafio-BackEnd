using MediatR;
using Moto.Domain.Entities;
using Moto.Domain.Primitives;

namespace Moto.Application.UseCases.Motorcycles.Queries;

/// <summary>
/// Query to retrieve all motorcycles, optionally filtered by license plate.
/// </summary>
/// <param name="Placa">Optional license plate to filter the motorcycles. If null, all motorcycles are returned.</param>
public sealed record GetAllMotrocycles(string? Placa) : IRequest<Result<List<Motorcycle>>>;