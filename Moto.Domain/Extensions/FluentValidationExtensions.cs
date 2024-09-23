using FluentValidation.Results;
using Moto.Domain.Primitives;

namespace Moto.Domain.Extensions;

public static class FluentValidationExtensions
{
    public static List<ValidationError> AsErrors(this ValidationResult valResult)
    {
        var resultErrors = new List<ValidationError>();

        foreach (var valFailure in valResult.Errors)
        {
            resultErrors.Add(new ValidationError(
                valFailure.PropertyName, 
                valFailure.ErrorMessage, 
                valFailure.ErrorCode
            ));
        }

        return resultErrors;
    }
}
