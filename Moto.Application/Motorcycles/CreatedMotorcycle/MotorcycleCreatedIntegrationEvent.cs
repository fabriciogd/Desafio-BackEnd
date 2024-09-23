using Moto.Application.Base;
using Moto.Domain.Events;
using System.Text.Json.Serialization;

namespace Moto.Application.Motorcycles.CreatedMotorcycle;

public sealed class MotorcycleCreatedIntegrationEvent : IIntegrationEvent
{ 
    public string Identificador { get; set; }

    internal MotorcycleCreatedIntegrationEvent(MotorcycleCreatedEvent motorcycleCreatedEvent)
    {
        Identificador = motorcycleCreatedEvent.Identificador;
    }

    [JsonConstructor]
    public MotorcycleCreatedIntegrationEvent(string identificador)
    {
        Identificador = identificador;
    }
}

