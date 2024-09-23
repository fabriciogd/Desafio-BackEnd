using FluentValidation;
using Moto.Domain.Extensions;
using Moto.Domain.Primitives;

namespace Moto.Domain.Base;

public abstract class ValueObject
{
    public List<ValidationError> Errors = new();

    protected bool OnValidate<TValidator, TEntity>()
        where TValidator : AbstractValidator<TEntity>, new()
        where TEntity : class
    {
        var validationResult = new TValidator().Validate(this as TEntity);
        Errors = validationResult.AsErrors();

        return validationResult.IsValid;
    }
    public bool IsValid => Errors.Count == 0;

    protected abstract bool Validate();
}