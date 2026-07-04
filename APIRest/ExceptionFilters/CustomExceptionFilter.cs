using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using APIRest.Exceptions;
using APIRest.DTOs;

namespace APIRest.ExceptionFilters;

public class CustomExceptionFilter(ILogger<CustomExceptionFilter> logger) : IExceptionFilter
{
    private readonly ILogger<CustomExceptionFilter> _logger = logger;

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "Ocorreu um erro não tratado na API: {Message}", context.Exception.Message);

        var statusCode = HttpStatusCode.InternalServerError;
        var friendlyMessage = "Desculpe, ocorreu um erro interno no servidor. Tente novamente mais tarde.";

        if (context.Exception is BusinessException businessException)
        {
            statusCode = businessException.StatusCode;
            friendlyMessage = businessException.Message;
        }

        var response = new BaseResponseErrorDto([friendlyMessage]);

        context.Result = new ObjectResult(response)
        {
            StatusCode = (int)statusCode
        };

        context.ExceptionHandled = true;
    }
}
