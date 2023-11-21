using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Driver.Common.Core;
using Driver.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Task = System.Threading.Tasks.Task;

namespace Driver.Api.MiddleWares
{
    /// <summary>
    /// Global Exception Middleware
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IConfiguration _configuration;
        private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="configuration"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
            _logger = loggerFactory?.CreateLogger<ExceptionMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>

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

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {


            var serializerSettings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };

            var exception = new
            {
                ex.Message,
                ex.StackTrace,
                ex.InnerException
            };

            var exceptionJson = JsonConvert.SerializeObject(exception, serializerSettings);

            context.Response.ContentType = "application/json";

            var detailedExceptionMessage = $"----------Exception---------{exceptionJson}---------";

            _logger.LogError($"{detailedExceptionMessage}");

            if (ex is BaseException baseException)
            {
                await HandleBaseExceptionAsync(context, baseException);
            }
            else if (ex is UnauthorizedAccessException)
            {
                await HandleUnAuthorizedExceptionAsync(context);
            }
            else if (ex is SqliteException dbException)
            {

                await HandleSqLiteExceptionAsync(context, dbException);

            }
            else
            {

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponse
                {
                    Message = _configuration["Enable_Stack_Trace"] == "TRUE" ? exceptionJson : ex.Message

                }));
            }
        }


        /// <summary>
        /// Handle Base Exception
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private async Task HandleBaseExceptionAsync(HttpContext context, BaseException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;

            await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponse() { Message = ex.Message  }));
        }



        /// <summary>
        /// Handle Unauthorized Exception
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task HandleUnAuthorizedExceptionAsync(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

            await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponse
            {
                Message = "Unauthorized",
                Status = HttpStatusCode.Unauthorized
            }));
        }

        /// <summary>
        /// Handle Sql Lite Exception
        /// </summary>
        /// <param name="context"></param>
        /// <param name="dbException"></param>
        /// <returns></returns>
        private async Task HandleSqLiteExceptionAsync(HttpContext context, SqliteException dbException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            switch (dbException.SqliteErrorCode)
            {
                case 19:
                    {
                        var error = new ErrorResponse
                        {
                            Status = HttpStatusCode.BadRequest,
                            Message = "Duplicate Primary Key (Id) For Entity"
                        };

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
                        break;
                    }
                default:
                    {

                        var error = new ErrorResponse
                        {
                            Status = HttpStatusCode.BadRequest,
                            Message = dbException.Message
                        };
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
                        break;
                    }
            }
        }
    }
}


