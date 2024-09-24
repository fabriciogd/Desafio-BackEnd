using Moto.Domain.Base;
using Moto.Domain.Enums;
using Moto.Domain.Validators;

namespace Moto.Domain.Entities;

public class Rental : BaseEntity
{
    public int CourierId { get; private set; }

    public int MotorcycleId { get; private set; }

    public int PlanId { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime EndDate { get; private set; }

    public DateTime ExpectedEndDate { get; private set; }

    public RentStatusEnum Status { get; private set; }

    public decimal? TotalPayment { get; private set; }

    public virtual Plan Plan { get; private set; }

    public virtual Motorcycle Motorcycle { get; private set; }

    public virtual Courier Courier { get; private set; }

    private Rental(
        int courierId,
        int motorcycleId,
        int planId,
        DateTime startDate,
        DateTime endDate,
        DateTime expectedEndDate)
    {
        CourierId = courierId;
        MotorcycleId = motorcycleId;
        PlanId = planId;
        StartDate = startDate;
        EndDate = endDate;
        ExpectedEndDate = expectedEndDate;

        Validate();
    }

    public static Rental Create(
        int courierId,
        int motorcycleId,
        int planId,
        DateTime startDate,
        DateTime endDate,
        DateTime expectedEndDate) =>
            new(courierId, motorcycleId, planId, DateTime.Now.AddDays(1), endDate, expectedEndDate);

    public void Complete(DateTime endDate)
    {
        EndDate = endDate;
        Status = RentStatusEnum.Finished;

        Validate();

        TotalPayment = CalculateBaseCoast() + CalculateFee();
    }

    private decimal CalculateFee()
    {
        var notEffectedDays = EndDate.Subtract(StartDate).Days;

        if (notEffectedDays == 0) return 0;

        return notEffectedDays > 0 ?
            Plan.CostPerDay * Plan.Fee * notEffectedDays :
            Math.Abs(notEffectedDays) * 50.0M;
    }

    private decimal CalculateBaseCoast()
    {
        DateTime endDate = EndDate >= ExpectedEndDate ? ExpectedEndDate : EndDate;

        int totalDays = endDate.Subtract(StartDate).Days;

        return Plan.CostPerDay * totalDays;
    }

    protected override bool Validate()
    {
        return OnValidate<RentalValidator, Rental>();
    }
}