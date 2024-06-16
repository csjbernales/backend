using backend.api.Data;
using backend.api.Data.Generated;
using backend.api.Middleware;
using backend.api.Models;
using backend.api.Models.Generated;
using backend.api.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
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

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
});

builder.Services.AddHealthChecks();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "", Version = "v1" });
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
            Email = "csjbernales@gmail.com"
        },
        License = new OpenApiLicense
        {
            Name = "Apache 2.0",
            Url = new Uri(builder.Configuration.GetSection("SwaggerDoc")["LicenseUrl"]!)
        }
    }
);
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("v1/swagger.json", "Backend API v1");
    });
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandler>();

app.UseHealthChecks("/health");
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();


[ExcludeFromCodeCoverage]
partial class Program { }