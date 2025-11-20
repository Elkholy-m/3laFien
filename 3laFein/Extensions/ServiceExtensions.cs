using Contracts;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using NLog;
using Repository;
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
                    config => config.MigrationsAssembly("3laFein")
                );
            });
        }

        public static void  ConfigLoggerService(this IServiceCollection services)
        {
            LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
    }
}
