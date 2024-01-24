using Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace Exchange.WebAPI.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
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
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (ex)
                {
                    case InvalidRequestException:
                        response.StatusCode = (int)HttpStatusCode.UnprocessableContent;
                        break;

                    case NoDataAvailableException:
                        response.StatusCode = (int)HttpStatusCode.UnprocessableContent;
                        break;

                    case NoPreviousDayDataException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    default:
                        _logger.LogError(ex, ex.Message);
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = ex?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
