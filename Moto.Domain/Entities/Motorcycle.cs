using Moto.Domain.Base;
using Moto.Domain.Events;

namespace Moto.Domain.Entities;

public sealed class Motorcycle: BaseEntity
{
    public string? Identificador { get; private set; }
    public short Ano { get; private set; }
    public string? Modelo { get; private set; }
    public string? Placa { get; private set; }

    public Motorcycle()
    {

    }

    private Motorcycle(string? identificador, short ano, string? modelo, string? placa)
    {
        Identificador = identificador;
        Ano = ano;
        Modelo = modelo;
        Placa = placa;

        AddDomainEvent(new MotorcycleCreatedEvent(identificador, ano, modelo, placa));
    }

    public static Motorcycle Create(
        string? identificador, short ano, string? modelo, string? placa) =>
            new Motorcycle(identificador, ano, modelo, placa);

    public void UpdatePlate(string placa) => Placa = placa;
}
