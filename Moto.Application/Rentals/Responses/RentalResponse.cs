namespace Moto.Application.Rents.Responses;

public sealed record RentalResponse(
    int Id,
    int EntregadorId, 
    int MotoId, 
    int Plano,
    DateTime DataInicio, 
    DateTime DataTermino,
    DateTime DataPrevisaoTermino,
    decimal? ValorTotal
);