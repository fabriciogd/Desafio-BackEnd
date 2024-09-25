using Moto.Domain.Base;
using Moto.Domain.Validators;

namespace Moto.Domain.ValueObjects;
public class DrivingLicense : ValueObject
{
    public DrivingLicense(string value)
    {
        Value = value;

        Validate();
    }

    public string? Value { get; private set; }
    public static DrivingLicense Create(string? value) => new(value);

    protected override bool Validate()
    {
        return OnValidate<DrivingLicenseValidator, DrivingLicense>();
    }

    public static implicit operator string(DrivingLicense cnpj) => cnpj.Value;

    public static implicit operator DrivingLicense(string value) => new(value);
}