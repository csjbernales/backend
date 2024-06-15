using backend.api.Data;
using backend.api.Data.Generated;
using backend.api.Middleware;
using backend.api.Models;
using backend.api.Models.Generated;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Mvc;
using backend.api;
using backend.api.Repository.Interfaces;
[assembly: ApiController]

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

builder.Services.AddScoped<ICustomer, Customer>();
builder.Services.AddScoped<IProduct, Product>();

SqlConnection connection = new(new CustomSqlConnectionStringBuilder(dbProps).ConnectionString());

builder.Services.AddDbContext<FullstackDBContext>(options =>
        options.UseSqlServer(connection));

builder.Services.AddControllers(o =>
{
    o.UseRoutePrefix("api");
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
}).ConfigureApiBehaviorOptions(options =>
{
    var builtInFactory = options.InvalidModelStateResponseFactory;

    options.InvalidModelStateResponseFactory = context =>
    {
        var logger = context.HttpContext.RequestServices
                            .GetRequiredService<ILogger<Program>>();
        logger.Log(LogLevel.Warning, context.HttpContext.Request.Path, "Failed to request on specific endpoint");
        return builtInFactory(context);
    };
});

builder.Services.AddHealthChecks();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandler>();

app.UseHealthChecks("/health");
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();


[ExcludeFromCodeCoverage]
partial class Program { }