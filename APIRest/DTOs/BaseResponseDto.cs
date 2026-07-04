using static System.Runtime.InteropServices.JavaScript.JSType;

namespace APIRest.DTOs;

public class BaseResponseDto<T>(bool error, List<string> errorMessages, T result)
{
    public bool Error { get; set; } = error;
    public List<string> ErrorMessages { get; set; } = errorMessages;
    public T Result { get; set; } = result;
}

public class BaseResponseErrorDto(List<string> errorMessages)
{
    public bool Error { get; } = true;
    public List<string> ErrorMessages { get; set; } = errorMessages;
    public object? Result { get; } = null;
}
