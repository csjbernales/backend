using backend.api.Data;
using backend.api.Data.Generated;
using backend.api.Middleware;
using backend.api.Models;
using backend.api.Models.Generated;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var conn = builder.Configuration.GetSection("ConnectionString");

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
builder.Services.AddSingleton<ErrorModel>();

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