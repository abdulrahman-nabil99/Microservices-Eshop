using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handler
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        private ILogger<CustomExceptionHandler> _logger;
        public CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) 
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError($"Error Message: {exception.Message}, Time of occurrance {DateTime.Now}");
            var details = exception switch
            {
                InternalServerException => new ExceptionDetails
                {
                    Details = exception.Message,
                    Title = exception.GetType().Name,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Instance = httpContext.Request.Path,
                },
                BadRequestException => new ExceptionDetails
                {
                    Details = exception.Message,
                    Title = exception.GetType().Name,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Instance = httpContext.Request.Path,
                },
                NotFoundException => new ExceptionDetails
                {
                    Details = exception.Message,
                    Title = exception.GetType().Name,
                    StatusCode = StatusCodes.Status404NotFound,
                    Instance = httpContext.Request.Path,
                },
                ValidationException => new ExceptionDetails
                {
                    Details = exception.Message,
                    Title = exception.GetType().Name,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Instance = httpContext.Request.Path,
                },
                _ => new ExceptionDetails
                {
                    Details = exception.Message,
                    Title = exception.GetType().Name,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Instance = httpContext.Request.Path,
                }
            };
            details.Extensions.Add("traceId", httpContext.TraceIdentifier);
            if (exception is ValidationException vex)
            {
                details.Extensions.Add("validationErrors", vex.Errors);
            }
            await httpContext.Response.WriteAsJsonAsync(details, cancellationToken: cancellationToken);
            return true;
        }
    }
}
