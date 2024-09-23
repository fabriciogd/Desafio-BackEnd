namespace Moto.Domain.Primitives;

public class Result: Result<Result>
{
    public Result() : base() { }
    protected internal Result(ResultStatus status) : base(status) { }

    public static Result Success() => new();

    public static Result Success(string successMessage) => new() { SuccessMessage = successMessage };

    public static Result Created() => new(ResultStatus.Created);

    public static Result<T> Success<T>(T value) => new(value);

    public static Result<T> Created<T>(T value) => Result<T>.Created(value);

    public static Result<T> Invalid<T>(IEnumerable<ValidationError> validationErrors)
        => Result<T>.Invalid(validationErrors);

    public new static Result Invalid(IEnumerable<ValidationError> validationErrors)
        => new(ResultStatus.Invalid) { ValidationErrors = validationErrors };

    public static Result Conflict(params string[] errorMessages) => new(ResultStatus.Conflict) { Errors = errorMessages };
    public static Result NotFound(params string[] errorMessages) => new(ResultStatus.NotFound) { Errors = errorMessages };}