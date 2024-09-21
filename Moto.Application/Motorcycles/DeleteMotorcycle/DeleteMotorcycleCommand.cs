using MediatR;

namespace Moto.Application.Motorcycles.DeleteMotorcycle;

public sealed record DeleteMotorcycleCommand(string Id): IRequest<Unit>;
