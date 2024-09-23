using MediatR;
using Moto.Domain.Primitives;

namespace Moto.Application.Motorcycles.Commands;

public sealed record CreateMotorcycle(short Ano, string? Modelo, string? Placa) : IRequest<Result>;