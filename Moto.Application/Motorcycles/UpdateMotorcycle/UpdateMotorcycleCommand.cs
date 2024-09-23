using MediatR;
using System.Text.Json.Serialization;

namespace Moto.Application.Motorcycles.UpdateMotorcycle;

public sealed record UpdateMotorcycleCommand([property: JsonIgnore] int Id, string Placa) : IRequest;