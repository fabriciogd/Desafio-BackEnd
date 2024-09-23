using MediatR;
using Moto.Domain.Primitives;

namespace Moto.Application.Rentals.Commands;

public sealed record CreateRental(
    int EntregadorId,
    int MotoId,
    int Plano,
    DateTime DataInicio,
    DateTime DataTermino,
    DateTime DataPrevisaoTermino
) : IRequest<Result>;