using backend.api.Models;
using System.Security.Claims;

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
        /// Invoke
        /// </summary>
        /// <param name="context">context</param>
        /// <returns>next middleware in pipeline</returns>
        public async Task Invoke(HttpContext context)
        {
            if (context.User.Identity is not null && context.User.Identity.IsAuthenticated)
            {
                ClaimsIdentity? claimsIdentity = context.User.Identity as ClaimsIdentity;
                bool hasRequiredClaim = claimsIdentity?.HasClaim(claim => claim.Issuer == "https://dev-48yt3g6lxsb6vr82.eu.auth0.com/") ?? false;

                if (hasRequiredClaim)
                {
                    //hehe
                }
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }

            logger.Log(LogLevel.Information, "Request initiated: | {RequestMethod} | {RequestPath}", context.Request.Method, context.Request.Path);
            await next(context);
        }
    }
}
