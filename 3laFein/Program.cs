using _3laFein.Extensions;
using Contracts;
using NLog;
using Service.Contracts;
using Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigCors();
builder.Services.ConfigIIS();
builder.Services.ConfigSqlServer(builder.Configuration);
builder.Services.ConfigLoggerService();
builder.Services.ConfigEmailService(builder.Configuration);
builder.Services.ConfigAppSettings(builder.Configuration);
builder.Services.AddIdentityHandlersAndStores()
                .ConfigureIdentityOptions()
                .AddIdentityAuth(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

await app.SeedIdentityAsync();

// Configure the HTTP request pipeline.

app.UseCors("CorsPolicy");
app.AddIdentityAuthMiddlewares();

app.MapControllers();

app.MigrateDatabase(app.Services.GetRequiredService<ILoggerManager>());

app.MapGroup("/api")
   .MapIdentityUserEndpoints()
   .MapGoogleAuthEndpoints();
app.Run();
