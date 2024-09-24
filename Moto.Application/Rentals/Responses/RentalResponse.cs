namespace Moto.Application.Rents.Responses;

/// <summary>
/// Represents the response data for a rental transaction.
/// This record encapsulates all the necessary details regarding a rental.
/// </summary>
/// <param name="Id">The unique identifier of the rental transaction.</param>
/// <param name="EntregadorId">The identifier of the courier associated with the rental.</param>
/// <param name="MotoId">The identifier of the motorcycle being rented.</param>
/// <param name="Plano">The identifier of the rental plan associated with the transaction.</param>
/// <param name="DataInicio">The start date of the rental.</param>
/// <param name="DataTermino">The end date of the rental, if completed; otherwise, null.</param>
/// <param name="DataPrevisaoTermino">The expected end date of the rental.</param>
/// <param name="ValorTotal">The total amount payable for the rental, if available; otherwise, null.</param>
public sealed record RentalResponse(
    int Id,
    int EntregadorId, 
    int MotoId, 
    int Plano,
    DateOnly DataInicio,
    DateOnly? DataTermino,
    DateOnly DataPrevisaoTermino,
    decimal? ValorTotal
);