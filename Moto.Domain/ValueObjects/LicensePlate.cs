using Moto.Domain.Base;
using Moto.Domain.Validators;

namespace Moto.Domain.ValueObjects;

public class LicensePlate : ValueObject
{
    public LicensePlate(string value)
    {
        Value = value;

        Validate(); 
    }

    public string? Value { get; private set; }
    public static LicensePlate Create(string? value) => new(value);

    protected override bool Validate()
    {
        return OnValidate<LicensePlateValidator, LicensePlate>();
    }

    public static implicit operator string(LicensePlate licensePlate) => licensePlate.Value;

    public static implicit operator LicensePlate(string value) => new(value);
}