using ClanChat.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ClanChat.Integration.HttpApi.Filters;

internal sealed class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public ApiExceptionFilterAttribute() =>
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(NotFoundException), HandleNotFoundException },
            { typeof(OperationIsForbiddenException), HandleOperationIsForbiddenException },
            { typeof(ClanIsNotEmptyException), HandleClanIsNotEmptyException }
        };
    
    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }
    
    private void HandleException(ExceptionContext context)
    {
        var type = context.Exception.GetType();
        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }

        if (!context.ModelState.IsValid)
        {
            HandleInvalidModelStateException(context);
            return;
        }

        HandleUnknownException(context);
    }
    
    private static void HandleUnknownException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An error occurred while processing your request.",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;
    }

    private static void HandleInvalidModelStateException(ExceptionContext context)
    {
        var details = new ValidationProblemDetails(context.ModelState)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }
    
    private static void HandleNotFoundException(ExceptionContext context)
    {
        var exception = context.Exception as NotFoundException;

        var details = new ProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found",
            Detail = exception!.Message,
            Status = 404
        };

        context.Result = new NotFoundObjectResult(details);

        context.ExceptionHandled = true;
    }
    
    private static void HandleOperationIsForbiddenException(ExceptionContext context)
    {
        var exception = context.Exception as OperationIsForbiddenException;

        var details = new ProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "Operation is forbidden",
            Detail = exception!.Message,
            Status = 403,
        };

        context.Result = new ObjectResult(details);

        context.ExceptionHandled = true;
    }
    
    private static void HandleClanIsNotEmptyException(ExceptionContext context)
    {
        var exception = context.Exception as ClanIsNotEmptyException;

        var details = new ProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "Clan is not empty",
            Detail = exception!.Message,
            Status = 400,
        };

        context.Result = new ObjectResult(details);

        context.ExceptionHandled = true;
    }
}