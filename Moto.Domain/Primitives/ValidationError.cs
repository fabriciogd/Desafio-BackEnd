namespace Moto.Domain.Primitives;

public class ValidationError
{
    public string PropertyName { get; set; }
    public string ErrorMessage { get; set; }
    public string ErrorCode { get; set; }

    public ValidationError(string propertyname, string errorMessage, string errorCode)
    {
        PropertyName = propertyname;
        ErrorMessage = errorMessage;
        ErrorCode = errorCode;
    }
}
