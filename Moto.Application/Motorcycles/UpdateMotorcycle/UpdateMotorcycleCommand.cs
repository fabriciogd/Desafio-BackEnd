using MediatR;
using System.Text.Json.Serialization;

namespace Moto.Application.Motorcycles.UpdateMotorcycle;

public sealed record UpdateMotorcycleCommand([property: JsonIgnore] string? Id, string Placa) : IRequest;