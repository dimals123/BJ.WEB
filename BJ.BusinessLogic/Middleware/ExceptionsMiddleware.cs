using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace BJ.BusinessLogic.Middleware
{
    public class ExceptionsMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await _next(context);
            }
            catch(ValidationException ex)
            {
                var message = ex.Message;
                var statusCode = HttpStatusCode.BadRequest;
                await HandleExceptionAsync(context, ex, statusCode, message);
            }
            catch (Exception ex)
            {
                var message = "Internal Server Error.";
                var statusCode = HttpStatusCode.InternalServerError;
                await HandleExceptionAsync(context, ex, statusCode, message);
            }

        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode, string message)
        {

            var result = JsonConvert.SerializeObject(new
            {
                error = message
            });
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(result);
        }
    }
}
