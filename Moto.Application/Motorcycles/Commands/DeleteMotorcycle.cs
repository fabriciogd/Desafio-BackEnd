using MediatR;
using Moto.Domain.Primitives;

namespace Moto.Application.Motorcycles.Commands;

public sealed record DeleteMotorcycle(int Id) : IRequest<Result>;
