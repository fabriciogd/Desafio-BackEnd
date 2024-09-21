using Moto.Domain.Base;
using Moto.Domain.Events;

namespace Moto.Domain.Entities;

public sealed class Motorcycle: BaseEntity
{
    public string? Identificador { get; protected set; }
    public short Ano { get; protected set; }
    public string? Modelo { get; protected set; }
    public string? Placa { get; protected set; }

    public Motorcycle(string? identificador, short ano, string? modelo, string? placa)
    {
        Identificador = identificador;
        Ano = ano;
        Modelo = modelo;
        Placa = placa;

        AddDomainEvent(new MotorcycleCreatedEvent(identificador, ano, modelo, placa));
    }

    public void UpdatePlate(string placa) => Placa = placa;
}
