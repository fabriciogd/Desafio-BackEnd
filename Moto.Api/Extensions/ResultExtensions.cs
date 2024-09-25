using Microsoft.AspNetCore.Mvc;
using Moto.Api.Models;
using Moto.Domain.Primitives;

namespace Moto.Api.Extensions;

internal static class ResultExtensions
{
    public static IActionResult ToHttpNonSuccessResult(this Moto.Domain.Primitives.IResult result)
    {
        var errors = result.Errors.Select(error => new ApiErrorResponse(error)).ToList();

        switch (result.Status)
        {
            case ResultStatus.NotFound:
                return new NotFoundObjectResult(ApiResponse.NotFound(errors));

            case ResultStatus.Conflict:
                return new ConflictObjectResult(ApiResponse.BadRequest(errors));

            case ResultStatus.Invalid:

                var validationErrors = result
                    .ValidationErrors
                    .Select(validation => new ApiErrorResponse(validation.ErrorMessage));

                return new BadRequestObjectResult(ApiResponse.BadRequest(validationErrors));
            default:
                return new BadRequestObjectResult(ApiResponse.BadRequest(errors));
        }
    }
}