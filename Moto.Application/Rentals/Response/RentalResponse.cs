namespace Moto.Application.Rents.Response;

public sealed record RentalResponse(
    int EntregadorId, 
    int MotoId, 
    DateTime DataInicio, 
    DateTime DataTermino,
    DateTime DataPrevisaoTermino
);