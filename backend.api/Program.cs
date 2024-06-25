using backend.api.Auth;
using backend.api.Configurations;
using backend.api.Middleware;
using backend.api.Service;
using backend.api.Service.Interfaces;
using backend.data.Data;
using backend.data.Data.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

[assembly: ApiController]
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
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

app.MapControllers().RequireAuthorization();

await app.RunAsync();

[ExcludeFromCodeCoverage]
internal static partial class Program
{ }