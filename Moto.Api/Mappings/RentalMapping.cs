using Moto.Application.UseCases.Rentals.Responses;
using Moto.Domain.Entities;

namespace Moto.Api.Mappings;
public static class RentalMapping
{
    public static RentalResponse ToResponse(this Rental rental)
        => new RentalResponse(
                rental.Id, 
                rental.CourierId, 
                rental.MotorcycleId, 
                rental.PlanId, 
                rental.StartDate, 
                rental.EndDate, 
                rental.ExpectedEndDate, 
                rental.TotalPayment
            );
}