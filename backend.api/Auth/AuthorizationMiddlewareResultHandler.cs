using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using System.Net;

namespace backend.api.Auth
{
    public class AuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler //todo
    {
        private readonly Microsoft.AspNetCore.Authorization.Policy.AuthorizationMiddlewareResultHandler DefaultHandler = new();

        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            if (Show404ForForbiddenResult(authorizeResult))
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }

            await DefaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }

        private static bool Show404ForForbiddenResult(PolicyAuthorizationResult policyAuthorizationResult)
        {
            return policyAuthorizationResult.Forbidden &&
                   policyAuthorizationResult.AuthorizationFailure!.FailedRequirements
                       .OfType<Show404Requirement>().Any();
        }
    }

    public class Show404Requirement : IAuthorizationRequirement
    { }
}