using global::ToDoApp.Services.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using ToDoApp.Services.Exceptions;

namespace ToDoApp.Api.Middlewares
{

        public class ExceptionMiddleware
        {
            private readonly RequestDelegate _next;

            public ExceptionMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public async Task InvokeAsync(HttpContext httpContext)
            {
                try
                {
                    await _next(httpContext);
                }
                catch (Exception ex)
                {
                    await HandleExceptionAsync(httpContext, ex);
                }
            }

            private static Task HandleExceptionAsync(HttpContext context, Exception exception)
            {
                context.Response.ContentType = "application/json";

                var statusCode = exception switch
                {
                    TaskNotFoundException => HttpStatusCode.NotFound,
                    InvalidStatusTransitionException => HttpStatusCode.BadRequest,
                    _ => HttpStatusCode.InternalServerError,
                };

                context.Response.StatusCode = (int)statusCode;

                var result = JsonSerializer.Serialize(new
                {
                    error = exception.Message,
                    statusCode = context.Response.StatusCode
                });

                return context.Response.WriteAsync(result);
            }
        }

}
