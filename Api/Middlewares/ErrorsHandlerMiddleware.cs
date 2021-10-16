using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Errors;
using Business_Logic.Exceptions;
using Business_Logic.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Api.Middlewares
{
    public class ErrorsHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorsHandlerMiddleware> logger;
        
        public ErrorsHandlerMiddleware(RequestDelegate next, ILogger<ErrorsHandlerMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                HttpResponse response = context.Response;
                response.ContentType = "application/json";

                string message = null;

                switch (exception)
                {
                    case NotFoundRestException ex:
                        response.StatusCode = (int) HttpStatusCode.NotFound;
                        message = ex.Message;
                        if (ex.Errors != null)
                        {
                            logger.LogError(string.Join("\n      ", ex.Errors.Select(e => e.StringifyError())));
                        } 
                        logger.LogError(ex, message);
                        break;
                    case BadRequestRestException ex:
                        response.StatusCode = (int) HttpStatusCode.BadRequest;
                        message = ex.Message;
                        if (ex.Errors != null)
                        {
                            logger.LogError(string.Join("\n      ", ex.Errors.Select(e => e.StringifyError())));
                        } 
                        logger.LogError(ex, message);
                        break;
                    default:
                        response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        logger.LogError(exception, "Something went wrong");
                        break;
                }

                await response.WriteAsync(new ErrorResponse
                {
                    StatusCode = response.StatusCode,
                    Message = message ?? "Internal Server Error"
                }.ToString());
            }
        }
    }
}