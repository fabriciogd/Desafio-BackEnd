using Moto.Domain.Base;
using Moto.Domain.Enums;

namespace Moto.Domain.Entities;

public class Rental : BaseEntity
{
    public int CourierId { get; private set; }  

    public int MotorcycleId { get; private set; }

    public int PlanId { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime EndDate { get; private set; }
    
    public DateTime ExpectedEndData { get; private set; }

    public RentStatusEnum Status { get; private set; }

    public decimal? TotalPayment { get; private set; }

    public virtual Plan Plan { get; private set; }

    public virtual Motorcycle Motorcycle { get; private set; }

    public virtual Courier Courier { get; private set; }
}
