namespace Moto.Application.Couriers.Responses;

public sealed record CourierResponse(
    int Id,
    string? CNPJ,
    DateTime DataNascimento,
    string? NumeroCNH,
    string? TipoCNH,
    string? CaminhoImagemCNH);