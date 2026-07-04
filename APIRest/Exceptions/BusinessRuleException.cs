using System.Net;

namespace APIRest.Exceptions;

public class BusinessRuleException(string message) : BusinessException(message, HttpStatusCode.BadRequest);
