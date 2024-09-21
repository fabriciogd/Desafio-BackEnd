using Moto.Domain.Base;

namespace Moto.Domain.Entities;

public sealed class Courier: BaseEntity
{
    public string? Identificador { get; private set; }
    public string? CNPJ { get; private set; }
    public DateOnly DataNascimento { get; private set; }
    public string? NumeroCNH { get; private set; }
    public string? TipoCNH { get; private set; }
    public string? ImagemCNH { get; private set; }

    public Courier()
    {

    }

    private Courier(string? identificador, string? cnpj, DateOnly dataNascimento, string? numeroCnh, string? tipoCnh)
    {
        Identificador = identificador;
        CNPJ = cnpj;
        DataNascimento = dataNascimento;
        NumeroCNH = numeroCnh;
        TipoCNH = tipoCnh;
    }

    public static Courier Create(
        string? identificador, string? cnpj, DateOnly dataNascimento, string? numeroCnh, string? tipoCnh) => 
            new Courier(identificador, cnpj, dataNascimento, numeroCnh, tipoCnh);

    public void UpdateImagemCnh(string path) => ImagemCNH = path;
}
