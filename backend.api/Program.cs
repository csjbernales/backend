using backend.api.Auth;
using backend.api.Data;
using backend.api.Data.Generated;
using backend.api.Middleware;
using backend.api.Models;
using backend.api.Models.Generated;
using backend.api.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Text.Json.Serialization;

[assembly: ApiController]
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//Add services to the container.

IConfigurationSection conn = builder.Configuration.GetSection("ConnectionString");

ConnectionStrings dbProps = new()
{
    DataSource = conn["DataSource"]!,
    Database = conn["Database"]!,
    IntegratedSecurity = bool.Parse(conn["IntegratedSecurity"]!),
    ConnectTimeout = int.Parse(conn["ConnectTimeout"]!),
    Encrypt = bool.Parse(conn["Encrypt"]!),
    TrustServerCertificate = bool.Parse(conn["TrustServerCertificate"]!),
    ApplicationIntent = conn["ApplicationIntent"]!,
    MultiSubnetFailover = bool.Parse(conn["MultiSubnetFailover"]!)
};

SqlConnection connection = new(new CustomSqlConnectionStringBuilder(dbProps).ConnectionString());

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
});

builder.Services.AddScoped<ICustomer, Customer>();
builder.Services.AddScoped<IProduct, Product>();
builder.Services.AddDbContext<FullstackDBContext>(options =>
        options.UseSqlServer(connection));
builder.Services.AddHealthChecks();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
    new OpenApiInfo
    {
        Title = "Backend API - V1",
        Version = "v1",
        Description = "A simple API for fullstack .net app",
        TermsOfService = new Uri(builder.Configuration.GetSection("SwaggerDoc")["TosUrl"]!),
        Contact = new OpenApiContact
        {
            Name = "Jhon B",
            Email = "email"
        },
        License = new OpenApiLicense
        {
            Name = "Apache 2.0",
            Url = new Uri(builder.Configuration.GetSection("SwaggerDoc")["LicenseUrl"]!)
        }
    });

    // Add the Bearer token security definition
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // Require Bearer token for all endpoints
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

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

//--------------------------------------------------------------------------------------------------

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("v1/swagger.json", "Backend API - V1");
    });
}

app.UseHttpsRedirection();

app.UseHealthChecks("/health");

app.UseAuthorization();

app.UseMiddleware<ExceptionHandler>();

app.MapControllers().RequireAuthorization();

await app.RunAsync();

[ExcludeFromCodeCoverage]
internal partial class Program
{ }