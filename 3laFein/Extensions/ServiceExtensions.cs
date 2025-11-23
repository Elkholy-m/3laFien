using Contracts;
using EmailService;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLog;
using Repository;
using Service;
using Service.Contracts;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;

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
                    x => x.MigrationsAssembly("3laFein")
                );
            });
        }

        public static void  ConfigLoggerService(this IServiceCollection services)
        {
            LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigEmailConfiguration(this IServiceCollection services, IConfiguration config)
        {
            var emailConfig = config.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            emailConfig!.Password = Environment.GetEnvironmentVariable("IbnBatotaPass");
            services.AddSingleton(emailConfig!);
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

        public static void ConfigIdentity(this IServiceCollection servicces)
        {
            servicces.AddIdentity<User, IdentityRole<Guid>>(config =>
            {
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.Password.RequiredLength = 5;

                config.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<RepositoryContext>();
        }

        public static IServiceCollection ConfigAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            return services;
        }
    }
}
