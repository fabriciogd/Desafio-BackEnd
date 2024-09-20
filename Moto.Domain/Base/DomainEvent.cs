namespace Moto.Domain.Base;

public abstract class DomainEvent
{
    public DateTime OccurredOn { get; private init; } = DateTime.Now;
}
