using Microsoft.AspNetCore.Mvc;
using Moto.Api.Models;
using Moto.Domain.Primitives.Result;

namespace Moto.Api.Extensions;

internal static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(this Result<T> result) =>
       result.IsSuccess
           ? new OkObjectResult(ApiResponse<T>.Ok(result.Value))
           : result.ToHttpNonSuccessResult();

    private static IActionResult ToHttpNonSuccessResult<T>(this Result<T> result)
    {
        switch (result.Status)
        {
            case ResultStatus.Invalid:
                var validationErrors = result
                    .ValidationErrors
                    .Select(validation => new ApiErrorResponse(validation.ErrorMessage));

                return new BadRequestObjectResult(ApiResponse.BadRequest(validationErrors));

            default:
                return new BadRequestObjectResult(ApiResponse.BadRequest());
        }
    }
}
