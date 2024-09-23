using Moto.Domain.Base;
using Moto.Domain.Events;
using Moto.Domain.Validators;

namespace Moto.Domain.Entities;

public sealed class Motorcycle: BaseEntity
{    
    public short Year { get; private set; }
    public string? Model { get; private set; }
    public string? LicensePlate { get; private set; }

    private Motorcycle(short year, string? model, string? licensePlate)
    {
        Year = year;
        Model = model;
        LicensePlate = licensePlate;

        Validate();

        AddDomainEvent(new MotorcycleCreatedEvent(year, model, licensePlate));
    }

    public static Motorcycle Create(short year, string? model, string? licensePlate) =>
            new Motorcycle(year, model, licensePlate);

    public void UpdateLicensePlate(string licensePlate) => LicensePlate = licensePlate;

    protected override bool Validate()
    {
        return OnValidate<MotorcycleValidator, Motorcycle>();
    }
}
