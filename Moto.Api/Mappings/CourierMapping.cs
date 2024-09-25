using Moto.Application.UseCases.Couriers.Responses;
using Moto.Domain.Entities;

namespace Moto.Api.Mappings;
public static class CouriereMapping
{
    public static CourierResponse ToResponse(this Courier courier)
        => new CourierResponse(courier.Id, courier.Cnpj, courier.BirthDate, courier.DrivingLicense, courier.DrivingLicenseType, courier.DrivingLicenseImagePath);

    public static List<CourierResponse> ToResponse(this List<Courier> list)
        => list.Select(o => o.ToResponse()).ToList();
}