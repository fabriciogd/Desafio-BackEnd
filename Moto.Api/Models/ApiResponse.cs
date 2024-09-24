using System.Text.Json.Serialization;

namespace Moto.Api.Models;

/// <summary>
/// The `ApiResponse<T>` class is a generic response type that extends the <see cref="ApiResponse"/> class.
/// It is used to wrap both the result (of type `T`) and additional metadata such as success status, 
/// success message, and HTTP status code.
/// </summary>
public sealed class ApiResponse<T> : ApiResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApiResponse{T}"/> class with the result, success state,
    /// success message, and status code.
    /// </summary>
    /// <param name="result">The result of type <typeparamref name="T"/> returned by the API.</param>
    /// <param name="success">Indicates whether the operation was successful.</param>
    /// <param name="successMessage">Optional success message.</param>
    /// <param name="statusCode">HTTP status code for the response.</param>
    [JsonConstructor]
    public ApiResponse(T result, bool success, string successMessage, int statusCode)
        : base(success, successMessage, statusCode)
    {
        Result = result;
    }

    /// <summary>
    /// Initializes a new empty instance of the <see cref="ApiResponse{T}"/> class.
    /// This constructor can be used when values are not provided initially.
    /// </summary>
    public ApiResponse()
    {
    }

    /// <summary>
    /// Gets the result value of the response, which is of type <typeparamref name="T"/>.
    /// The result is set once during initialization and cannot be modified afterward.
    /// </summary>
    public T Result { get; private init; }

    /// <summary>
    /// Static factory method to create a successful response with HTTP status 200 (OK).
    /// </summary>
    /// <param name="result">The result of type <typeparamref name="T"/> returned by the API.</param>
    /// <returns>A new <see cref="ApiResponse{T}"/> indicating a successful operation.</returns>
    public static ApiResponse<T> Ok(T result) =>
        new() { Success = true, StatusCode = StatusCodes.Status200OK, Result = result };

    /// <summary>
    /// Static factory method to create a successful response with HTTP status 200 (OK)
    /// and a success message.
    /// </summary>
    /// <param name="result">The result of type <typeparamref name="T"/> returned by the API.</param>
    /// <param name="successMessage">The success message to include in the response.</param>
    /// <returns>A new <see cref="ApiResponse{T}"/> indicating a successful operation with a success message.</returns>
    public static ApiResponse<T> Ok(T result, string successMessage) =>
        new() { Success = true, StatusCode = StatusCodes.Status200OK, Result = result, SuccessMessage = successMessage };
}