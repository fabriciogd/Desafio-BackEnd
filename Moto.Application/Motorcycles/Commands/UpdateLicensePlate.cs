using MediatR;
using Moto.Domain.Primitives;
using System.Text.Json.Serialization;

namespace Moto.Application.Motorcycles.Commands;

public sealed record UpdateLicensePlate([property: JsonIgnore] int Id, string Placa) : IRequest<Result>;