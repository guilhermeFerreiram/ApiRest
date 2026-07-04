using System.Net;

namespace APIRest.Exceptions;

public abstract class BusinessException(string message, HttpStatusCode statusCode) : Exception(message)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
}
