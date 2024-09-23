using MediatR;

namespace Moto.Application.Motorcycles.DeleteMotorcycle;

public sealed record DeleteMotorcycleCommand(int Id): IRequest<Unit>;
