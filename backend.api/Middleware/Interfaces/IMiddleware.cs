namespace backend.api.Middleware.Interfaces
{
    /// <summary>
    /// Middleware interface
    /// </summary>
    public interface IMiddleware
    {
        /// <summary>
        /// Start of RD
        /// </summary>
        /// <param name="context">Http context</param>
        /// <returns>the next in call stack</returns>
        /// <exception cref="InvalidOperationException">400 Bad request</exception>
        Task Invoke(HttpContext context);
    }
}