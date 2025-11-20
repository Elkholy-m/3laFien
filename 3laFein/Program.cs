using _3laFein.Extensions;
using Contracts;
using NLog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigCors();
builder.Services.ConfigIIS();
builder.Services.ConfigSqlServer(builder.Configuration);
builder.Services.ConfigLoggerService();
builder.Services.ConfigEmailService(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase(app.Services.GetRequiredService<ILoggerManager>());
app.Run();
