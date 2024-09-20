namespace Moto.Domain.Primitives.Result;

public class Result<T>
{
    public Result(T value) => Value = value;

    protected Result(ResultStatus status) => Status = status;

    public T Value { get; init; }

    public ResultStatus Status { get; protected set; } = ResultStatus.Ok;

    public bool IsSuccess => Status is ResultStatus.Ok or ResultStatus.NoContent or ResultStatus.Created;

    public IEnumerable<ValidationError> ValidationErrors { get; protected set; } = [];

    public static Result<T> Invalid(IEnumerable<ValidationError> validationErrors)
        => new(ResultStatus.Invalid) { ValidationErrors = validationErrors };

    public static Result<T> Success(T value) => new(value);

    public static implicit operator T(Result<T> result) => result.Value;
    public static implicit operator Result<T>(T value) => new Result<T>(value);
}
