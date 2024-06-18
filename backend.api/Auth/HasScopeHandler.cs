using Microsoft.AspNetCore.Authorization;

namespace backend.api.Auth
{
    /// <summary>
    /// HasScopeHandler class
    /// </summary>
    public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
    {
        /// <summary>
        /// HandleRequirementAsync
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="requirement">requirement</param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
        {
            //if (!context.User.HasClaim(c => c.Issuer == requirement.Issuer))
            //{
            //    return Task.CompletedTask;
            //}

            //List<string> scopes = [.. context.User.FindFirst(c => c.Issuer == requirement.Issuer)!.Value.Split(' ')];

            //if (scopes.Count != 0)
            //{
            //    context.Succeed(requirement);
            //}

            //context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}