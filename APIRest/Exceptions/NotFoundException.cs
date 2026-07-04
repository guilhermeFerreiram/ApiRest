using System.Net;

namespace APIRest.Exceptions;

public class NotFoundException(string message) : BusinessException(message, HttpStatusCode.NotFound);
