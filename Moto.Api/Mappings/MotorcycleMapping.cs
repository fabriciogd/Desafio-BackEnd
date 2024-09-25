using Moto.Application.Motorcycles.Responses;
using Moto.Domain.Entities;

namespace Moto.Api.Mappings;

public static class MotorcycleMapping
{
    public static MotorcycleResponse ToResponse(this Motorcycle motorcycle)
        => new MotorcycleResponse(motorcycle.Id, motorcycle.Year, motorcycle.Model, motorcycle.LicensePlate);

    public static List<MotorcycleResponse> ToResponse(this List<Motorcycle> list)
        => list.Select(o => o.ToResponse()).ToList();
}
