using Moto.Domain.Base;

namespace Moto.Domain.Entities;

public class Plan: BaseEntity
{
    public short Days { get; private set; }

    public decimal CostPerDay { get; private set; }

    public decimal Fee { get; private set; }

    public Plan(int id, short days, decimal costPerDay, decimal fee) : this(days, costPerDay, fee)
    {
        Id = id;
    }

    public Plan(short days, decimal costPerDay, decimal fee)
    {
        Days = days;
        CostPerDay = costPerDay;
        Fee = fee;
    }
}