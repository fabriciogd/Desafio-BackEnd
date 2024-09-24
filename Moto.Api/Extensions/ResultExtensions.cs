using Microsoft.AspNetCore.Mvc;
using Moto.Api.Models;
using Moto.Domain.Primitives;

namespace Moto.Api.Extensions;

internal static class ResultExtensions
{
    /// <summary>
    /// Converts a custom Result object to an IActionResult.
    /// </summary>
    /// <param name="result">The Result object to convert.</param>
    /// <returns>An IActionResult representing the Result object.</returns>
    public static IActionResult ToActionResult(this Result result) =>
         result.IsSuccess
            ? new OkObjectResult(ApiResponse.Ok(result.SuccessMessage))
            : result.ToHttpNonSuccessResult();

    /// <summary>
    /// Converts a <see cref="Result{T}"/> to an <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T">The type of the result value.</typeparam>
    /// <param name="result">The result to convert.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result.</returns>
    public static IActionResult ToActionResult<T>(this Result<T> result) =>
       result.IsSuccess
          ? new OkObjectResult(ApiResponse<T>.Ok(result.Value))
          : result.ToHttpNonSuccessResult();

    private static IActionResult ToHttpNonSuccessResult(this Moto.Domain.Primitives.IResult result)
    {
        var errors = result.Errors.Select(error => new ApiErrorResponse(error)).ToList();

        switch (result.Status)
        {
            case ResultStatus.Created:
                return new StatusCodeResult(StatusCodes.Status201Created);

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