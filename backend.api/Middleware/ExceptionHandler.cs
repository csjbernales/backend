using backend.data.Models;

namespace backend.api.Middleware
{
    public class ExceptionHandler(RequestDelegate next, ILogger<ErrorModel> logger) : Interfaces.IMiddleware
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                logger.LogError(new EventId(1, "Exception handled error"), e, "InvalidOperationException");
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}