using backend.api.Models;

namespace backend.api.Middleware
{
    /// <summary>
    /// Interceptor handler middleware
    /// </summary>
    /// <param name="next">Request delegate</param>
    /// <param name="logger">Illoger service provided by microsoft</param>
    public class Interceptor(RequestDelegate next, ILogger<ErrorModel> logger) : Interfaces.IMiddleware
    {
        /// <summary>
        /// Start of RD
        /// </summary>
        /// <param name="context">Http context</param>
        /// <returns>the next in call stack</returns>
        public async Task Invoke(HttpContext context)
        {
            logger.Log(LogLevel.Information, "Request initiated: | {RequestMethod} | {RequestPath}", context.Request.Method, context.Request.Path);
            await next(context);
        }
    }
}