using _3laFein.Extensions;
using _3laFein.Reprsentaion;
using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using Service.Contracts;
using Service;
using Org.BouncyCastle.Crypto.Tls;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigCors();
builder.Services.ConfigIIS();
builder.Services.ConfigSqlServer(builder.Configuration);
builder.Services.ConfigLoggerService();
builder.Services.ConfigEmailConfiguration(builder.Configuration);
builder.Services.ConfigRepositoryManager();
builder.Services.ConfigServiceManager();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.ConfigIdentity();
builder.Services.ConfigEmailService(builder.Configuration);
builder.Services.ConfigAppSettings(builder.Configuration);
builder.Services.AddIdentityHandlersAndStores()
                .ConfigureIdentityOptions()
                .AddIdentityAuth(builder.Configuration);

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(opt =>
    {
        opt.SuppressModelStateInvalidFilter = true;
    })
    .AddApplicationPart(typeof(AssymblyRefrence).Assembly);
builder.Services.AddControllers();
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

await app.SeedIdentityAsync();

// Configure the HTTP request pipeline.
app.HandleExceptions();
if (app.Environment.IsProduction())
    app.UseHsts();

app.UseHttpsRedirection();
app.UseForwardedHeaders(new ForwardedHeadersOptions()
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseStaticFiles();

app.UseCors("CorsPolicy");
app.AddIdentityAuthMiddlewares();

app.MapControllers();

app.MigrateDatabase(app.Services.GetRequiredService<ILoggerManager>());

app.MapGroup("/api")
   .MapIdentityUserEndpoints()
   .MapGoogleAuthEndpoints();
app.Run();
