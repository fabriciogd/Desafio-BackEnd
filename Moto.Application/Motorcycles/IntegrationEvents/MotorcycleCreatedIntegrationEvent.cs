using Moto.Application.Base;
using Moto.Domain.Events;
using System.Text.Json.Serialization;

namespace Moto.Application.Motorcycles.IntegrationEvents;

public sealed class MotorcycleCreatedIntegrationEvent : IIntegrationEvent
{
    public int Id { get; set; }
    public int Year { get; set; }

    public string? Model { get; set; }

    public string? LicensePlate { get; set; }

    internal MotorcycleCreatedIntegrationEvent(MotorcycleCreatedEvent motorcycleCreatedEvent)
    {
        Id = motorcycleCreatedEvent.Motorcycle.Id;
        Year = motorcycleCreatedEvent.Motorcycle.Year;
        Model = motorcycleCreatedEvent.Motorcycle.Model;
        LicensePlate = motorcycleCreatedEvent.Motorcycle.LicensePlate;
    }

    [JsonConstructor]
    public MotorcycleCreatedIntegrationEvent(int id, int year, string? model, string? licensePlate)
    {
        Id = id;
        Year = year;
        Model = model;
        LicensePlate = licensePlate;
    }
}