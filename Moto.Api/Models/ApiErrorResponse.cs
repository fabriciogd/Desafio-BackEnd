namespace Moto.Api.Models
{
    public class ApiErrorResponse(string message)
    {
        public string Message { get; } = message;
    }
}
