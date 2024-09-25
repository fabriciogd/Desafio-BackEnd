using Moto.Domain.Base;
using Moto.Domain.Validators;
using Moto.Domain.ValueObjects;

namespace Moto.Domain.Entities;

public sealed class Courier : BaseEntity
{
    public Cnpj Cnpj { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public DrivingLicense DrivingLicense { get; private set; }
    public string? DrivingLicenseType { get; private set; }
    public string? DrivingLicenseImagePath { get; private set; }

    public Courier()
    {

    }

    private Courier(Cnpj cnpj, DateOnly birthDate, DrivingLicense drivingLicense, string? drivingLicenseType)
    {
        Cnpj = cnpj;
        BirthDate = birthDate;
        DrivingLicense = drivingLicense;
        DrivingLicenseType = drivingLicenseType;

        AddErrors(cnpj.Errors);
        AddErrors(drivingLicense.Errors);

        Validate();
    }

    public static Courier Create(Cnpj cnpj, DateOnly birthDate, DrivingLicense drivingLicense, string? drivingLicenseType) => 
            new Courier(cnpj, birthDate, drivingLicense, drivingLicenseType);

    public void UpdateDrivingLicenseImagePath(string drivingLicenseImagePath) => DrivingLicenseImagePath = drivingLicenseImagePath;

    protected override bool Validate()
    {
        return OnValidate<CourierValidator, Courier>();
    }
}
