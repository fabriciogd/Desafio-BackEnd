using MediatR;
using Moto.Domain.Primitives;
using System.Text.Json.Serialization;

namespace Moto.Application.Rentals.Commands;

/// <summary>
/// Represents a request to complete a rental transaction.
/// Contains the rental ID and the return date of the rented motorcycle.
/// </summary>
/// <param name="Id">The unique identifier of the rental to be completed.</param>
/// <param name="dataDevolucao">The date when the rented motorcycle is returned.</param>
public sealed record CompleteRental([property: JsonIgnore] int Id, DateOnly dataDevolucao) : IRequest<Result>;
