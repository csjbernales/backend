using backend.api.Auth;
using backend.api.Customers;
using backend.api.Customers.Interface;
using backend.api.Middleware;
using backend.data.Data;
using backend.data.Data.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

[assembly: ApiController]

Log.Logger = new LoggerConfiguration().WriteTo.Console()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .CreateBootstrapLogger();

try
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    builder.Services.AddSerilog((services, lc) => lc.ReadFrom.Configuration(builder.Configuration.GetSection("Serilog")).ReadFrom.Services(services).Enrich.FromLogContext().WriteTo.Console());

    //Add services to the container.

    IConfigurationSection conn = builder.Configuration.GetSection("ConnectionString");

    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.IncludeFields = true;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.IgnoreReadOnlyFields = true;
        options.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
    });

    builder.Services.AddDbContext<FullstackDBContext>(options => options.UseSqlServer(DbConnectionStringsBuilder.ConnectionBuilder(conn)));

    builder.Services.AddScoped<ICustomerService, CustomersService>();

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
            TermsOfService = new Uri(builder.Configuration["SwaggerDoc:TosUrl"]!),
            Contact = new OpenApiContact
            {
                Name = "Jhon B",
                Email = builder.Configuration["email"]
            },
            License = new OpenApiLicense
            {
                Name = "Apache 2.0",
                Url = new Uri(builder.Configuration["SwaggerDoc:LicenseUrl"]!)
            }
        });

        if (!builder.Environment.IsDevelopment())
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

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
        }
    });

    AuthConfig.AuthOptions(builder);

    //--------------------------------------------------------------------------------------------------

    WebApplication app = builder.Build();

    app.UseSerilogRequestLogging(options =>
    {
        options.MessageTemplate = "Handled {RequestPath}";

        options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;

        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        {
            diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
            diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
        };
    });

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("v1/swagger.json", "Backend API - V1");
        });
        app.UseDeveloperExceptionPage();
    }

    app.UseHttpsRedirection();

    app.UseHealthChecks("/health");

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseMiddleware<ExceptionHandler>();
    app.UseMiddleware<LoggingHandler>();

    app.MapControllers().RequireAuthorization();

    await app.RunAsync();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    await Log.CloseAndFlushAsync();
}

[ExcludeFromCodeCoverage]
internal static partial class Program
{ }

