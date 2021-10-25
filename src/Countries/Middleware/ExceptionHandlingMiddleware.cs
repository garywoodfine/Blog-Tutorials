using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Boleyn.Countries.Content.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Boleyn.Countries.Content.Middleware
{
    internal sealed class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                await HandleValidationException(context, e);
            }
        }

        private static async Task HandleValidationException(HttpContext httpContext, Exception exception)
        {
            var statusCode = GetStatusCode(exception);

            var response = new
            { 
                title = GetTitle(exception),
                status = statusCode,
                detail = exception.Message,
                errors = GetErrors(exception)
            };

            httpContext.Response.ContentType = "application/json";

            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        private static string GetTitle(Exception exception) =>
            exception switch
            {
                CountriesException ce => ce.Title,
                _ => "Server Error"
            };
        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                ValidationsException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };
        

        private static IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
        {
            IReadOnlyDictionary<string, string[]> errors = null;

            if (exception is ValidationsException validationException)
            {
                errors = validationException.Errors;
            }

            return errors;
        }
    }
}