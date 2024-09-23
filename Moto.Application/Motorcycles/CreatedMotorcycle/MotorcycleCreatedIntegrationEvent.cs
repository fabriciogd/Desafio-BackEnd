using Moto.Application.Base;
using Moto.Domain.Events;
using System.Text.Json.Serialization;

namespace Moto.Application.Motorcycles.CreatedMotorcycle;

public sealed class MotorcycleCreatedIntegrationEvent : IIntegrationEvent
{ 
    public string? Identificador { get; set; }

    public int Ano { get; set; }

    public string? Modelo { get; set; }

    public string? Placa { get; set; }

    internal MotorcycleCreatedIntegrationEvent(MotorcycleCreatedEvent motorcycleCreatedEvent)
    {
        Identificador = motorcycleCreatedEvent.Identificador;
        Ano = motorcycleCreatedEvent.Ano;
        Modelo = motorcycleCreatedEvent.Modelo;
        Placa = motorcycleCreatedEvent.Placa;
    }

    [JsonConstructor]
    public MotorcycleCreatedIntegrationEvent(string? identificador, int ano, string? modelo, string? placa)
    {
        Identificador = identificador;
        Ano = ano;
        Modelo = modelo;
        Placa = placa;
    }
}

