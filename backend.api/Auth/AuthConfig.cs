using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace backend.api.Auth
{
    public static class AuthConfig
    {
        public static void AuthOptions(WebApplicationBuilder builder, IEnvironmentWrapper environmentWrapper)
        {
            if (environmentWrapper.IsDevelopment())
            {
                builder.Services.AddAuthentication("Development").AddScheme<AuthenticationSchemeOptions, DevelopmentAuthenticationHandler>("Development", options => { });
            }
            else
            {
                builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}/";
                    options.Audience = builder.Configuration["Auth0:Audience"];
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = ClaimTypes.NameIdentifier
                    };
                });

                builder.Services.AddAuthorization();

                builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

                builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationMiddlewareResultHandler>();
            }
        }
    }
}