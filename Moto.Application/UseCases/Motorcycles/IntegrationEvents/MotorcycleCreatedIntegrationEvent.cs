using Moto.Application.Contracts.Event;
using Moto.Domain.Events;
using System.Text.Json.Serialization;

namespace Moto.Application.UseCases.Motorcycles.IntegrationEvents;

/// <summary>
/// Integration event representing the creation of a motorcycle.
/// This event is used to communicate the details of a newly created motorcycle across services.
/// </summary>
public sealed class MotorcycleCreatedIntegrationEvent : IIntegrationEvent
{
    public int Id { get; set; }
    public int Year { get; set; }

    public string? Model { get; set; }

    public string? LicensePlate { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MotorcycleCreatedIntegrationEvent"/> class
    /// using data from the <see cref="MotorcycleCreatedEvent"/>.
    /// </summary>
    /// <param name="motorcycleCreatedEvent">The event that contains motorcycle creation data.</param>
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