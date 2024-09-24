using System.Text.Json.Serialization;

namespace Moto.Api.Models;

/// <summary>
/// Represents a standard API response that includes information about success, status code, 
/// success message, and any errors that occurred during the API call.
/// </summary>
public class ApiResponse
{
    /// <summary>
    /// Gets a value indicating whether the API operation was successful.
    /// </summary>
    public bool Success { get; protected init; }

    /// <summary>
    /// Gets the message returned when the operation is successful. This is optional.
    /// </summary>
    public string SuccessMessage { get; protected init; } = string.Empty;

    /// <summary>
    /// Gets the HTTP status code for the response.
    /// </summary>
    public int StatusCode { get; protected init; }

    /// <summary>
    /// Gets the collection of errors returned when the operation fails.
    /// If the operation is successful, this will be an empty collection.
    /// </summary>
    public IEnumerable<ApiErrorResponse> Errors { get; protected init; } = [];

    /// <summary>
    /// Constructor used during JSON deserialization to initialize the response with a 
    /// success state, message, and HTTP status code.
    /// </summary>
    /// <param name="success">Indicates if the operation was successful.</param>
    /// <param name="successMessage">Optional success message returned on success.</param>
    /// <param name="statusCode">HTTP status code for the operation result.</param>
    [JsonConstructor]
    public ApiResponse(bool success, string successMessage, int statusCode)
    {
        Success = success;
        SuccessMessage = successMessage;
        StatusCode = statusCode;
    }

    /// <summary>
    /// Default constructor for creating an empty API response object.
    /// This can be used when no initial values need to be provided.
    /// </summary>
    public ApiResponse()
    {
    }

    /// <summary>
    /// Static factory method to create a successful response with HTTP status 201 (Created).
    /// </summary>
    /// <returns>An API response indicating a resource was successfully created.</returns>
    public static ApiResponse Created() =>
        new() { Success = true, StatusCode = StatusCodes.Status201Created };

    /// <summary>
    /// Static factory method to create a successful response with HTTP status 200 (OK)
    /// and an optional success message.
    /// </summary>
    /// <param name="successMessage">The success message to return with the response.</param>
    /// <returns>An API response indicating the operation was successful.</returns>
    public static ApiResponse Ok(string successMessage) =>
        new() { Success = true, StatusCode = StatusCodes.Status200OK, SuccessMessage = successMessage };

    /// <summary>
    /// Static factory method to create a bad request response with HTTP status 400 (Bad Request)
    /// and a collection of errors describing what went wrong.
    /// </summary>
    /// <param name="errors">A collection of errors to include in the response.</param>
    /// <returns>An API response indicating the request was invalid.</returns>
    public static ApiResponse BadRequest(IEnumerable<ApiErrorResponse> errors) =>
        new() { Success = false, StatusCode = StatusCodes.Status400BadRequest, Errors = errors };
}