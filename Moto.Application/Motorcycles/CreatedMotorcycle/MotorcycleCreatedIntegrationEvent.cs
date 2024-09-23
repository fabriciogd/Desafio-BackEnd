using Moto.Application.Base;
using Moto.Domain.Events;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Moto.Application.Motorcycles.CreatedMotorcycle;

public sealed class MotorcycleCreatedIntegrationEvent : IIntegrationEvent
{ 
    public int Year { get; set; }

    public string? Model { get; set; }

    public string? LicensePlate { get; set; }

    internal MotorcycleCreatedIntegrationEvent(MotorcycleCreatedEvent motorcycleCreatedEvent)
    {
        Year = motorcycleCreatedEvent.Year;
        Model = motorcycleCreatedEvent.Model;
        LicensePlate = motorcycleCreatedEvent.LicensePlate;
    }

    [JsonConstructor]
    public MotorcycleCreatedIntegrationEvent(int year, string? model, string? licensePlate)
    {
        Year = year;
        Model = model;
        LicensePlate = licensePlate;
    }
}