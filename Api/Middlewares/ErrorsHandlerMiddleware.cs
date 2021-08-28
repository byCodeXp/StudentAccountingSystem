using System;
using System.Net;
using System.Threading.Tasks;
using Api.Errors;
using Business_Logic.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Api.Middlewares
{
    public class ErrorsHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorsHandlerMiddleware> _logger;
        
        public ErrorsHandlerMiddleware(RequestDelegate next, ILogger<ErrorsHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                HttpResponse response = context.Response;
                response.ContentType = "application/json";

                string message = null;
                
                switch (exception)
                {
                    case HttpResponseException ex:
                        response.StatusCode = (int) HttpStatusCode.BadRequest;
                        message = ex.Message;
                        _logger.LogError(exception, message);
                        break;
                    default:
                        response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        _logger.LogError(exception, "Something went wrong");
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