using backend.data.Models;

namespace backend.api.Middleware
{
    public class LoggingHandler(RequestDelegate next, ILogger<ErrorModel> logger) : Interfaces.IMiddleware
    {
        public async Task Invoke(HttpContext context)
        {
            logger.LogInformation("Request initiated: | {RequestMethod} | {RequestPath}", context.Request.Method, context.Request.Path);
            await next(context);
        }
    }
}