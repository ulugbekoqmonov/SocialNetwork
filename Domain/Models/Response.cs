namespace Domain.Models;

public class Response<T>
{
    public Response(bool isSuccess, object errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public Response(T result)
    {
        Result = result;
    }
    public Response()
    {
    }
    public bool IsSuccess { get; set; } = true;
    public T? Result { get; set; }       
    public object Errors { get; set; }
}