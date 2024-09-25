namespace Moto.Application.UseCases.Couriers.Responses;

/// <summary>
/// Represents the response data for a courier, containing relevant details.
/// </summary>
/// <param name="Id">The unique identifier of the courier.</param>
/// <param name="Cnpj">The CNPJ (Cadastro Nacional da Pessoa Jurídica) of the courier.</param>
/// <param name="DataNascimento">The birth date of the courier.</param>
/// <param name="NumeroCnh">The driver's license number of the courier.</param>
/// <param name="TipoCnh">The type of driver's license held by the courier.</param>
/// <param name="CaminhoImagemCnh">The file path of the driver's license image.</param>
public sealed record CourierResponse(
    int Id,
    string? Cnpj,
    DateOnly DataNascimento,
    string? NumeroCnh,
    string? TipoCnh,
    string? CaminhoImagemCnh);