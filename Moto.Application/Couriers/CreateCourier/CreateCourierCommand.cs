using MediatR;

namespace Moto.Application.Couriers.CreateCourier;

public sealed record CreateCourierCommand(
    string? Identificador,
    string? CNPJ,
    DateTime DataNascimento,
    string? NumeroCNH,
    string? TipoCNH,
    string ImagemCNH) : IRequest<Unit>;