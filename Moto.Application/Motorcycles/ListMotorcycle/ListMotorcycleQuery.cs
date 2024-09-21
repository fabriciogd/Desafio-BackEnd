using MediatR;
using Moto.Application.Motorcycles.Response;

namespace Moto.Application.Motorcycles.ListMotorcycle;

public sealed record ListMotorcycleQuery(string Placa): IRequest<IEnumerable<MotorcycleResponse>>;