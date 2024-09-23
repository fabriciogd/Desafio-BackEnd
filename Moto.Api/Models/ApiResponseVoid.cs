using System.Text.Json.Serialization;

namespace Moto.Api.Models;

public class ApiResponse
{
    public bool Success { get; protected init; }
    public string SuccessMessage { get; protected init; } = string.Empty;
    public int StatusCode { get; protected init; }
    public IEnumerable<ApiErrorResponse> Errors { get; protected init; } = [];

    [JsonConstructor]
    public ApiResponse(bool success, string successMessage, int statusCode)
    {
        Success = success;
        SuccessMessage = successMessage;
        StatusCode = statusCode;
    }

    public ApiResponse()
    {
    }

    public static ApiResponse Created() =>
        new() { Success = true, StatusCode = StatusCodes.Status201Created };

    public static ApiResponse Ok(string successMessage) =>
        new() { Success = true, StatusCode = StatusCodes.Status200OK, SuccessMessage = successMessage };

    public static ApiResponse BadRequest(IEnumerable<ApiErrorResponse> errors) =>
        new() { Success = false, StatusCode = StatusCodes.Status400BadRequest, Errors = errors };

    internal static object? WithMessage(string message)
    {
        throw new NotImplementedException();
    }
}