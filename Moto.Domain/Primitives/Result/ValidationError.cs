namespace Moto.Domain.Primitives.Result;

public class ValidationError
{
    public ValidationError(string identifier, string errorMessage, string errorCode)
    {
        Identifier = identifier;
        ErrorMessage = errorMessage;
        ErrorCode = errorCode;
    }

    public string Identifier { get; private set; }
    public string ErrorMessage { get; private set; }
    public string ErrorCode { get; private set; }
}
