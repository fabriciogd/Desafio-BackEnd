using MediatR;
using Moto.Domain.Primitives;

namespace Moto.Application.Couriers.Commands;

public sealed record CreateCourier(
    string? CNPJ,
    DateTime DataNascimento,
    string? NumeroCNH,
    string? TipoCNH,
    string? ImagemCNH) : IRequest<Result>;