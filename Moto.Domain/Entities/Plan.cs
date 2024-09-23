using Moto.Domain.Base;

namespace Moto.Domain.Entities;

public class Plan : BaseEntity
{
    public decimal CostPerDay { get; private set; }

    public decimal Fee { get; private set; }

    public Plan(int id, decimal costPerDay, decimal fee) : this(costPerDay, fee)
    {
        Id = id;
    }

    public Plan(decimal costPerDay, decimal fee)
    {
        CostPerDay = costPerDay;
        Fee = fee;
    }

    protected override bool Validate()
    {
        throw new NotImplementedException();
    }
}