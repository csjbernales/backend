using Microsoft.AspNetCore.Authorization;

namespace backend.api.Auth
{
    public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "scope" && c.Issuer == requirement.Issuer))
                return Task.CompletedTask;

            List<string> scopes = [.. context.User.FindFirst(c => c.Type == "scope" && c.Issuer == requirement.Issuer)!.Value.Split(' ')];

            if (scopes.Exists(s => s == requirement.Scope))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
