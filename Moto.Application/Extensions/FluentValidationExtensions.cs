using FluentValidation.Results;
using Moto.Domain.Primitives.Result;

namespace Moto.Application.Extensions;

public static class FluentValidationExtensions
{
    public static List<ValidationError> AsErrors(this ValidationResult valResult)
    {
        return valResult.Errors.Select(a => new ValidationError(a.PropertyName, a.ErrorMessage, a.ErrorCode)).ToList();
    }
}
