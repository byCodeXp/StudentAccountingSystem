using Data_Transfer_Objects.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Middlewares
{
    // FIX: response has server errors
    
    public class ErrorHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandler> _logger;

        public ErrorHandler(RequestDelegate next, ILogger<ErrorHandler> logger)
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
            catch (Exception error)
            {
                HttpResponse response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case HttpResponseException:
                        response.StatusCode = (int) HttpStatusCode.BadRequest;
                        _logger.LogError(error, "Http response error");
                        break;
                    case KeyNotFoundException:
                        response.StatusCode = (int) HttpStatusCode.NotFound;
                        _logger.LogError(error, "Not found error");
                        break;
                    default:
                        response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        _logger.LogError(error, "Error");
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
