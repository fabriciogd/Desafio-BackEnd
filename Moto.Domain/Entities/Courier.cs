using Moto.Domain.Base;

namespace Moto.Domain.Entities;

public sealed class Courier: BaseEntity
{
    public string? Cnpj { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string? DrivingLicense { get; private set; }
    public string? DrivingLicenseType { get; private set; }
    public string? DrivingLicenseImagePath { get; private set; }

    private Courier(string? cnpj, DateTime birthDate, string? drivingLicense, string? drivingLicenseType)
    {
        Cnpj = cnpj;
        BirthDate = birthDate;
        DrivingLicense = drivingLicense;
        DrivingLicenseImagePath = drivingLicenseType;
    }

    public static Courier Create(string? cnpj, DateTime birthDate, string? drivingLicense, string? drivingLicenseType) => 
            new Courier(cnpj, birthDate, drivingLicense, drivingLicenseType);

    public void UpdateDrivingLicenseImagePath(string drivingLicenseImagePath) => DrivingLicenseImagePath = drivingLicenseImagePath;
}
