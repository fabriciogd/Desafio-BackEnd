namespace Moto.Domain.Primitives;

public class Result<T> : IResult
{
    public T Value { get; init; }
    public bool IsSuccess => Status == ResultStatus.Ok || Status == ResultStatus.Created;
    public string SuccessMessage { get; init; }
    public ResultStatus Status { get; protected set; } = ResultStatus.Ok;
    public IEnumerable<ValidationError> ValidationErrors { get; protected set; } = [];
    public IEnumerable<string> Errors { get; protected set; } = [];
    protected Result() { }
    public Result(T value) => Value = value;

    protected Result(ResultStatus status) => Status = status;

    public static Result<T> Success(T value) => new(value);


    /// <summary>
    /// Represents a successful operation that resulted in the creation of a new resource.
    /// </summary>
    /// <typeparam name="T">The type of the resource created.</typeparam>
    /// <returns>A Result<typeparamref name="T"/> with status Created.</returns>
    public static Result<T> Created(T value) => new(ResultStatus.Created) { Value = value };

    /// <summary>
    /// Represents validation errors that prevent the underlying service from completing.
    /// </summary>
    /// <param name="validationErrors">A list of validation errors encountered</param>
    /// <returns>A Result<typeparamref name="T"/></returns>
    public static Result<T> Invalid(IEnumerable<ValidationError> validationErrors)
            => new(ResultStatus.Invalid) { ValidationErrors = validationErrors };


    /// <summary>
    /// Represents the situation where a service was unable to find a requested resource.
    /// Error messages may be provided and will be exposed via the Errors property.
    /// </summary>
    /// <param name="errorMessages">A list of string error messages.</param>
    /// <returns>A Result<typeparamref name="T"/></returns>
    public static Result<T> NotFound(params string[] errorMessages) => new(ResultStatus.NotFound) { Errors = errorMessages };
    
   
    /// <summary>
    /// Represents an error that occurred during the execution of the service.
    /// A single error message may be provided and will be exposed via the Errors property.
    /// </summary>
    /// <param name="errorMessage"></param>
    /// <returns></returns>
    public static Result<T> Error(string errorMessage) => new(ResultStatus.Error) { Errors = [errorMessage] };

}