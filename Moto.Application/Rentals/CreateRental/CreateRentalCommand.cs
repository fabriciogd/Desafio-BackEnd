using MediatR;

namespace Moto.Application.Rents.CreateRent;

public sealed record CreateRentalCommand(
    string EntregadorId, 
    string MotoId, 
    int Plano,
    DateTime DataInicio,
    DateTime DataTermino,
    DateTime DataPrevisaoTermino
): IRequest<Unit>;