namespace Moto.Api.Models;

public class ApiErrorResponse
{
    public ApiErrorResponse(string message)
    {
        Message = message;
    }

    public string? Message { get; set; }
}
