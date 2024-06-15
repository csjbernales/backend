using backend.api.Middleware.Interfaces;
using backend.api.Models;

namespace backend.api.Middleware
{
    public class Interceptor(RequestDelegate next, ILogger<ErrorModel> logger) : IExceptionHandler, IInterceptor
    {
        public async Task Invoke(HttpContext context)
        {
            logger.Log(LogLevel.Information, "Request initiated: | {RequestMethod} | {RequestPath}", context.Request.Method, context.Request.Path);
            await next(context);
        }
    }
}