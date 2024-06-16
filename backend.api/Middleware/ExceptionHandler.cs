using backend.api.Models;

namespace backend.api.Middleware
{
    /// <summary>
    /// Exception handler middleware
    /// </summary>
    /// <param name="next">Request delegate</param>
    /// <param name="logger">Illoger service provided by microsoft</param>
    public class ExceptionHandler(RequestDelegate next, ILogger<ErrorModel> logger) : Interfaces.IMiddleware
    {
        /// <summary>
        /// Start of RD
        /// </summary>
        /// <param name="context">Http context</param>
        /// <returns>the next in call stack</returns>
        /// <exception cref="InvalidOperationException">400 Bad request</exception>
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