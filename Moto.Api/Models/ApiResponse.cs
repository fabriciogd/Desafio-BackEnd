namespace Moto.Api.Models;

public  class ApiResponse
{
    public ApiResponse()
    {
    }

    public bool Success { get; protected init; }
    public int StatusCode { get; protected init; }

    public IEnumerable<ApiErrorResponse> Errors { get; private init; } = [];

    public static ApiResponse BadRequest(IEnumerable<ApiErrorResponse> errors) =>
       new() { Success = false, StatusCode = StatusCodes.Status400BadRequest, Errors = errors };
    public static ApiResponse BadRequest() =>
       new() { Success = false, StatusCode = StatusCodes.Status400BadRequest };
}