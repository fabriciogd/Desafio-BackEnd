namespace Moto.Api.Models;

/// <summary>
/// The <see cref="ApiErrorResponse"/> class represents an error message returned by the API. 
/// It contains a single message that provides details about the error.
/// </summary>
public class ApiErrorResponse
{
    /// <summary>
    /// Gets the error message returned by the API.
    /// </summary>
    public string Message { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiErrorResponse"/> class with the specified error message.
    /// </summary>
    /// <param name="message">The error message to be returned in the API response.</param>
    public ApiErrorResponse(string message)
    {
        Message = message;
    }
}