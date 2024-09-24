using MediatR;
using Moto.Domain.Primitives;
using System.Text.Json.Serialization;

namespace Moto.Application.Motorcycles.Commands;

/// <summary>
/// Represents a command to update the license plate of a motorcycle.
/// </summary>
/// <param name="Id">The unique identifier of the motorcycle. This property is ignored in the JSON serialization.</param>
/// <param name="Placa">The new license plate to be assigned to the motorcycle.</param>
public sealed record UpdateLicensePlate([property: JsonIgnore] int Id, string Placa) : IRequest<Result>;