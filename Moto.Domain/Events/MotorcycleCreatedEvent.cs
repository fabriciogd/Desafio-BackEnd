using Moto.Domain.Base;
using Moto.Domain.Entities;

namespace Moto.Domain.Events;

public class MotorcycleCreatedEvent: DomainEvent
{
    public Motorcycle Motorcycle { get; private set; }

    public MotorcycleCreatedEvent(Motorcycle motorcycle) => Motorcycle = motorcycle;
}
