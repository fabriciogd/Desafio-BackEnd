using Moto.Domain.Base;
using Moto.Domain.Events;
using Moto.Domain.Validators;
using Moto.Domain.ValueObjects;

namespace Moto.Domain.Entities;

public sealed class Motorcycle: BaseEntity
{    
    public short Year { get; private set; }
    public string? Model { get; private set; }
    public LicensePlate LicensePlate { get; private set; }

    public Motorcycle()
    {

    }

    private Motorcycle(short year, string? model, LicensePlate licensePlate)
    {
        Year = year;
        Model = model;
        LicensePlate = licensePlate;

        Validate();
        AddErrors(licensePlate.Errors);

        AddDomainEvent(new MotorcycleCreatedEvent(this));
    }

    public static Motorcycle Create(short year, string? model, LicensePlate licensePlate) =>
            new Motorcycle(year, model, licensePlate);

    public void UpdateLicensePlate(LicensePlate licensePlate) => LicensePlate = licensePlate;

    protected override bool Validate()
    {
        return OnValidate<MotorcycleValidator, Motorcycle>();
    }
}
