using backend.api.Auth;
using backend.api.Configurations;
using backend.api.Middleware;
using backend.api.Service;
using backend.api.Service.Interfaces;
using backend.data.Data;
using backend.data.Data.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    EnvironmentWrapper environmentWrapper = new(builder);

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

    builder.Services.AddSwaggerGen(Swagger.SwaggerOptions(builder, environmentWrapper));

    AuthConfig.AuthOptions(builder, environmentWrapper);

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

