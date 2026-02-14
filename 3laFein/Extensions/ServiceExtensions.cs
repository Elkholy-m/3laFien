using Contracts;
using EmailService;
using Entities.Models;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog;
using Repository;
using Service;
using Service.Contracts;

namespace _3laFein.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigCors(this IServiceCollection services)
        {
            services.AddCors(config =>
            {
                config.AddPolicy("CorsPolicy", configPolicy =>
                {
                    configPolicy.AllowAnyMethod();
                    configPolicy.AllowAnyOrigin();
                    configPolicy.AllowAnyHeader();
                });
            });
        }

        public static void ConfigIIS (this IServiceCollection services)
        {
            services.Configure<IISOptions>(opt =>
            {
                opt.AutomaticAuthentication = true;
                opt.AuthenticationDisplayName = null;
            });
        }

        public static void ConfigSqlServer(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<RepositoryContext>(opt =>
            {
                opt.UseSqlServer(
                    config.GetConnectionString("sqlConnection"),
                    x => x.UseNetTopologySuite()
                          .MigrationsAssembly("3laFein")
                );
            });

            services.AddDbContext<PlaceDbContext>(opt => {
                    opt.UseSqlServer(config.GetConnectionString("placeConnection"),
                            x => x.MigrationsAssembly("3laFein"));
                    });
        }

        public static void  ConfigLoggerService(this IServiceCollection services)
        {
            LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigEmailConfiguration(this IServiceCollection services, IConfiguration config)
        {
            // todo: Get Email App Pass From Windows
            var emailConfig = new EmailConfiguration();
            config.GetSection("EmailConfiguration").Bind(emailConfig);
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailSender>();
        }

        public static void ConfigRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void ConfigServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
        }

        public static IServiceCollection ConfigAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            return services;
        }

        public static void ConfigSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Final Project API",
                    Version = "v1",
                });

                setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Place to add JWT with Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    { new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference 
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer", 
                        },
                        new List<string>() 
                    }
                });
            });

        }

        public static void ConfigHttpClients(this IServiceCollection services) {
            services.AddHttpClient("Nominatim", client =>
                    {
                    client.BaseAddress = new Uri("https://nominatim.openstreetmap.org/");
                    client.DefaultRequestHeaders.UserAgent.ParseAdd(
                            "MyApp/1.0 (midoelkholy03@gmail.com)"
                            );
                    });
            services.AddHttpClient("geo-locations", client => {
                    client.BaseAddress = new Uri("https://cdn.geo-locations.com/");
                    });
        }
    }
}
