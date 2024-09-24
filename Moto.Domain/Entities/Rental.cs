using Moto.Domain.Base;
using Moto.Domain.Enums;
using Moto.Domain.Validators;

namespace Moto.Domain.Entities;

public class Rental : BaseEntity
{
    public int CourierId { get; private set; }

    public int MotorcycleId { get; private set; }

    public int PlanId { get; private set; }

    public DateOnly StartDate { get; private set; }

    public DateOnly? EndDate { get; private set; }

    public DateOnly ExpectedEndDate { get; private set; }

    public RentStatusEnum Status { get; private set; }

    public decimal? TotalPayment { get; private set; }

    public virtual Plan Plan { get; private set; }

    public virtual Motorcycle Motorcycle { get; private set; }

    public virtual Courier Courier { get; private set; }

    private Rental(
        int courierId,
        int motorcycleId,
        int planId,
        DateOnly startDate,
        DateOnly expectedEndDate)
    {
        CourierId = courierId;
        MotorcycleId = motorcycleId;
        PlanId = planId;
        StartDate = startDate;
        ExpectedEndDate = expectedEndDate;

        Validate();
    }

    public static Rental Create(
        int courierId,
        int motorcycleId,
        int planId,
        DateOnly startDate,
        DateOnly expectedEndDate) =>
            new(courierId, motorcycleId, planId, startDate, expectedEndDate);

    public void Complete(DateOnly endDate)
    {
        EndDate = endDate;
        Status = RentStatusEnum.Finished;

        Validate();

        TotalPayment = CalculateBaseCoast() + CalculateFee();
    }

    public void UpdatePlan(Plan plan) => Plan = plan;

    private decimal CalculateFee()
    {
        var notEffectedDays = EndDate.Value.DayNumber -  ExpectedEndDate.DayNumber;

        if (notEffectedDays == 0) return 0;

        return notEffectedDays < 0 ?
            Plan.CostPerDay * Plan.Fee * Math.Abs(notEffectedDays) :
            Math.Abs(notEffectedDays) * 50.0M;
    }

    private decimal CalculateBaseCoast()
    {
        var endDate = EndDate >= ExpectedEndDate ? ExpectedEndDate : EndDate.Value;

        int totalDays = endDate.DayNumber - StartDate.DayNumber;

        return Plan.CostPerDay * totalDays;
    }

    protected override bool Validate()
    {
        return OnValidate<RentalValidator, Rental>();
    }
}