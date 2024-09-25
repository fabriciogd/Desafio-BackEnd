namespace Moto.Domain.Primitives;

public class Result: Result<Result>
{
    public Result() : base() { }
    protected internal Result(ResultStatus status) : base(status) { }

    public static Result Success() => new();

    public static Result<T> Success<T>(T value) => new(value);

    public static Result<T> Created<T>(T value) => Result<T>.Created(value);

    public new static Result Invalid(IEnumerable<ValidationError> validationErrors)
        => new(ResultStatus.Invalid) { ValidationErrors = validationErrors };

    public new static Result NotFound(params string[] errorMessages) => new(ResultStatus.NotFound) { Errors = errorMessages };
    
    public new static Result Error(params string[] errorMessages) => new(ResultStatus.Error) { Errors = errorMessages };
}