using System.Net;

namespace APIRest.Exceptions;

public class DuplicateRegisterException(string message) : BusinessException(message, HttpStatusCode.Conflict);
