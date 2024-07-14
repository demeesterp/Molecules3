using FluentValidation;
using Molecules.Shared.Exceptions;
using Molecules.Shared.Logger;
using MoleculesWebApp.Handlers.Model;

namespace MoleculesWebApp.Handlers
{
    public static class GlobalExceptionHandler
    {

        public static async Task  HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            GetLogger(httpContext).LogError(exception, "An exception was handled by the global exception handler");
            if (exception is ValidationException validationException)
            {

                var validationError = new ServiceValidationError()
                {
                    ValidationErrors = validationException
                                        .Errors
                                        .Select(x => new ServiceValidationErrorItem(x.ErrorMessage, x.PropertyName))
                                        .ToList()
                };

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                await httpContext.Response.WriteAsJsonAsync(validationError);
            }
            else if (exception is DbResourceNotFoundException notFoundException)
            {
                GetLogger(httpContext).LogFatal(exception, "An unhandled excpetion ");
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                await httpContext.Response.WriteAsJsonAsync(new ServiceError(notFoundException.Message));
            }
            else
            {
                GetLogger(httpContext).LogFatal(exception, "An unhandled excpetion ");
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsJsonAsync(new ServiceError());
            }
        }


        private static IMoleculesLogger GetLogger(HttpContext context)
        {
            return (IMoleculesLogger)context.RequestServices.GetService(typeof(IMoleculesLogger))!;
        }

    }
}
