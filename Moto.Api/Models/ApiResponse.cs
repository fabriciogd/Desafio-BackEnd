using System.Text.Json.Serialization;

namespace Moto.Api.Models;

/// <summary>
/// The <see cref="ApiResponse"/> represents a standard API response that includes information about success, 
/// status code and any errors that occurred during the API call.
/// </summary>
public class ApiResponse
{

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
    /// <param name="statusCode">HTTP status code for the operation result.</param>
    [JsonConstructor]
    public ApiResponse(int statusCode)
    {
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
    /// Static factory method to create a bad request response with HTTP status 400 (Bad Request)
    /// and a collection of errors describing what went wrong.
    /// </summary>
    /// <param name="errors">A collection of errors to include in the response.</param>
    /// <returns>An API response indicating the request was invalid.</returns>
    public static ApiResponse BadRequest(IEnumerable<ApiErrorResponse> errors) =>
        new() { StatusCode = StatusCodes.Status400BadRequest, Errors = errors };

    /// <summary>
    /// Static factory method to create a not found request response with HTTP status 404 (Not Found)
    /// and a collection of errors describing what went wrong.
    /// </summary>
    /// <param name="errors">A collection of errors to include in the response.</param>
    /// <returns>An API response indicating the request was invalid.</returns>
    public static ApiResponse NotFound(IEnumerable<ApiErrorResponse> errors) =>
       new() { StatusCode = StatusCodes.Status404NotFound, Errors = errors };
}