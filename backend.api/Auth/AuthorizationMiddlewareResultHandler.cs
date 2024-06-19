using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using System.Net;

namespace backend.api.Auth
{
    public class AuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly Microsoft.AspNetCore.Authorization.Policy.AuthorizationMiddlewareResultHandler DefaultHandler = new();

        public async Task HandleAsync(RequestDelegate next, HttpContext context,
            AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            if (Show404ForForbiddenResult(authorizeResult))
            {
                // Return a 404 to make it appear as if the resource does not exist.
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }

            // Fallback to the default implementation.
            await DefaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }

        bool Show404ForForbiddenResult(PolicyAuthorizationResult policyAuthorizationResult)
        {
            return policyAuthorizationResult.Forbidden &&
                   policyAuthorizationResult.AuthorizationFailure!.FailedRequirements
                       .OfType<Show404Requirement>().Any();
        }
    }

    public class Show404Requirement : IAuthorizationRequirement { }
}