using Moto.Domain.Base;

namespace Moto.Domain.Events;

public class MotorcycleCreatedEvent: DomainEvent
{
    public short Year { get; private set; }
    public string? Model { get; private set; }
    public string? LicensePlate { get; private set; }

    public MotorcycleCreatedEvent(short year, string? model, string? licensePlate)
    {
        Year = year;
        Model = model;
        LicensePlate = licensePlate;
    }
}
