using MediatR;

namespace Moto.Application.Rents.CreateRent;

public sealed record CreateRentalCommand(
    int EntregadorId, 
    int MotoId, 
    int Plano,
    DateTime DataInicio,
    DateTime DataTermino,
    DateTime DataPrevisaoTermino
): IRequest<Unit>;