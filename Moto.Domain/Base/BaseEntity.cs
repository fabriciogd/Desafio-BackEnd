using FluentValidation;
using Moto.Domain.Extensions;
using Moto.Domain.Primitives;

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

    public List<ValidationError> Errors = new();

    protected void AddErrors(IReadOnlyCollection<ValidationError> errors)
       => Errors.AddRange(errors);

    protected bool OnValidate<TValidator, TEntity>()
        where TValidator : AbstractValidator<TEntity>, new()
        where TEntity : BaseEntity
    {
        var validationResult = new TValidator().Validate(this as TEntity);
        AddErrors(validationResult.AsErrors());

        return validationResult.IsValid;
    }
    public bool IsValid => Errors.Count == 0;

    protected abstract bool Validate();
}
