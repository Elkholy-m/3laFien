using Contracts;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Reflection.Metadata.Ecma335;

namespace _3laFein.Extensions
{
    public static class MigrationMangaer
    {
        public static WebApplication MigrateDatabase(this WebApplication app, ILoggerManager logger)
        {
            using (var scope = app.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<RepositoryContext>())
                {
                    try
                    {
                        appContext.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex.Message);
                        throw;
                    }
                }
            }
            return app;
        }
    }
}
