using MediatR;

namespace Moto.Domain.Base;

public abstract class DomainEvent : INotification
{
    public DateTime OccurredOn { get; private init; } = DateTime.Now;
}
