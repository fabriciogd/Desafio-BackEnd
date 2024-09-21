namespace Moto.Api.Models;

public  class ApiResponse
{
    public ApiResponse()
    {
    }

    public string Mensagem { get; private set; }

    public static ApiResponse WithMessage(string message) =>
        new() { Mensagem = message };
}