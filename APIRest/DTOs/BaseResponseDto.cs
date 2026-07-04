namespace APIRest.DTOs;

public class BaseResponseDto<T>(bool error, List<string> errorMessages, T result)
{
    public bool Error { get; set; } = error;
    public List<string> ErrorMessages { get; set; } = errorMessages;
    public T Result { get; set; } = result;
}
