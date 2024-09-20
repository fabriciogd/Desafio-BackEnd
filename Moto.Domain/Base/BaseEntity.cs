namespace Moto.Domain.Base;

public abstract class BaseEntity
{
    private readonly List<DomainEvent> _domainEvents = [];

    public int Id { get; set; }

    public IEnumerable<DomainEvent> DomainEvents =>
        _domainEvents.AsReadOnly();

    protected void AddDomainEvent(DomainEvent domainEvent) =>
        _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() =>
        _domainEvents.Clear();
}
