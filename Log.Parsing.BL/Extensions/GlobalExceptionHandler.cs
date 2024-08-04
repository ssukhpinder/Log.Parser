using Log.Parser.BL.Models.Response;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace Log.Parser.BL.Extensions
{
    public static class GlobalExceptionExtension
    {
        /// <summary>
        /// A extension method for global exception handler
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandler>();

            return app;
        }
    }
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(
            RequestDelegate next,
            ILogger<GlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Method to handle any exception triggered across app
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                ResponseMetaData<string> responseMetadata = new()
                {
                    Status = HttpStatusCode.InternalServerError,
                    IsError = true,
                    ErrorDetails = exception.Message
                };

                if (exception is BadHttpRequestException apiException)
                {
                    responseMetadata.Status = HttpStatusCode.BadRequest;
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                }

                var serializedResponseMetadata = JsonConvert.SerializeObject(responseMetadata);
                _logger.LogError(exception, "Exception occurred: {Message}", JsonConvert.SerializeObject(serializedResponseMetadata));
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(serializedResponseMetadata);
            }
        }

    }
}
