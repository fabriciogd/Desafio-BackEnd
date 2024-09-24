using MediatR;
using Moto.Domain.Primitives;

namespace Moto.Application.Motorcycles.Commands;

/// <summary>
/// Represents a command to create a new motorcycle.
/// </summary>
/// <param name="Ano">The year of the motorcycle.</param>
/// <param name="Modelo">The model of the motorcycle.</param>
/// <param name="Placa">The license plate of the motorcycle.</param>
public sealed record CreateMotorcycle(short Ano, string? Modelo, string? Placa) : IRequest<Result>;