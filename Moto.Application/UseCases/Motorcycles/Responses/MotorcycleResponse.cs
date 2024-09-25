namespace Moto.Application.UseCases.Motorcycles.Responses;

/// <summary>
/// Represents a response containing details of a motorcycle.
/// </summary>
/// <param name="Id">The unique identifier of the motorcycle.</param>
/// <param name="Ano">The year of manufacture of the motorcycle.</param>
/// <param name="Modelo">The model of the motorcycle.</param>
/// <param name="Placa">The license plate of the motorcycle.</param>
public sealed record MotorcycleResponse(int Id, int Ano, string? Modelo, string? Placa);
