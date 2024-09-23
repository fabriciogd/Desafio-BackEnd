using MediatR;
using Moto.Application.Motorcycles.Response;

namespace Moto.Application.Motorcycles.GetMotorcycle;

public sealed record GetMotorcycleCommand(int Id) : IRequest<MotorcycleResponse>;
