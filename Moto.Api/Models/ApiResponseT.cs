namespace Moto.Api.Models
{
    public class ApiResponse<T> : ApiResponse
    {
        public T? Result { get; private init; }

        public static ApiResponse<T> Ok(T result) =>
            new() { Success = true, StatusCode = StatusCodes.Status200OK, Result = result };
    }
}
