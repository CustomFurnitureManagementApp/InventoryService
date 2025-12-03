using InventoryService.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace InventoryService.Api.Middleware
{
    public class ErrorHandlingMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = HttpStatusCode.InternalServerError;
            var message = exception.Message;

            if (exception is NotFoundException)
                statusCode = HttpStatusCode.NotFound;

            context.Response.StatusCode = (int)statusCode;

            var response = JsonSerializer.Serialize(new { Message = message });
            return context.Response.WriteAsync(response);
        }
    }
}
