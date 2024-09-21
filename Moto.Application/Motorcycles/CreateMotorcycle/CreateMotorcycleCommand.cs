using MediatR;

namespace Moto.Application.Motorcycles.CreateMotorcycle;

public sealed record CreateMotorcycleCommand(
    string? Identificador, short Ano, string? Modelo, string? Placa) : IRequest<Unit>;