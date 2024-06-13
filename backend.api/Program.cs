using backend.api.Data;
using backend.api.Middleware;
using backend.api.Models;
using backend.api.Models.Generated;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<FullstackDBContext>();
builder.Services.AddScoped<ICustomer, Customer>();

builder.Services.AddSingleton<ErrorModel>();

builder.Services.AddControllers().AddJsonOptions(options => {
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