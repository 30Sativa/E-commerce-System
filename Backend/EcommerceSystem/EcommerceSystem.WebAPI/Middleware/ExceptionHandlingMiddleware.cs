using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EcommerceSystem.Application.Common;
using EcommerceSystem.Application.Common.Exceptions;


namespace EcommerceSystem.WebAPI.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex) // ❌ FluentValidation
            {
                _logger.LogWarning("Validation failed: {@Errors}", ex.Errors);

                await WriteResponse(
                    context,
                    HttpStatusCode.BadRequest,
                    "Validation failed",
                    ex.Errors.Select(e => new {
                        Field = e.PropertyName,
                        Error = e.ErrorMessage
                    })
                );
            }
            catch (BusinessException ex) // ❌ Business logic error
            {
                _logger.LogWarning(ex, "Business error");

                await WriteResponse(
                    context,
                    (HttpStatusCode)ex.StatusCode,
                    "Business error",
                    new { Detail = ex.Message }
                );
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Not found");

                await WriteResponse(
                    context,
                    HttpStatusCode.NotFound,
                    "Not found",
                    new { Detail = ex.Message }
                );
            }
            catch (UnauthorizedException ex)
            {
                _logger.LogWarning(ex, "Unauthorized");

                await WriteResponse(
                    context,
                    HttpStatusCode.Unauthorized,
                    "Unauthorized",
                    new { Detail = ex.Message }
                );
            }
            catch (ForbiddenException ex)
            {
                _logger.LogWarning(ex, "Forbidden");

                await WriteResponse(
                    context,
                    HttpStatusCode.Forbidden,
                    "Forbidden",
                    new { Detail = ex.Message }
                );
            }
            catch (ConflictException ex)
            {
                _logger.LogWarning(ex, "Conflict");

                await WriteResponse(
                    context,
                    HttpStatusCode.Conflict,
                    "Conflict",
                    new { Detail = ex.Message }
                );
            }
            catch (DbUpdateException ex) // ❌ Database error
            {
                _logger.LogError(ex, "Database update error");

                await WriteResponse(
                    context,
                    HttpStatusCode.Conflict,
                    "A database error occurred",
                    new { Detail = ex.InnerException?.Message ?? ex.Message }
                );
            }
            catch (Exception ex) // ❌ Unhandled
            {
                _logger.LogError(ex, "Unhandled exception");

                var env = context.RequestServices.GetRequiredService<IWebHostEnvironment>();

                object errorData;
                if (env.IsDevelopment())
                {
                    errorData = new
                    {
                        ex.Message,
                        ex.Source,
                        ex.StackTrace,
                        Inner = ex.InnerException?.Message
                    };
                }
                else
                {
                    errorData = new { Detail = "Internal server error" };
                }

                await WriteResponse(
                    context,
                    HttpStatusCode.InternalServerError,
                    "Internal server error",
                    errorData
                );
            }
        }

        private async Task WriteResponse(HttpContext context, HttpStatusCode statusCode, string message, object? data)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var response = BaseResponse<object>.FailResponse(message, data);

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
        }
    }
}
