using System.Text.Json;
using Contracts;
using Entities.PlaceDBModels;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace _3laFein.Extensions
{
    public static class MigrationMangaer {
        public static async Task<WebApplication> MigrateDatabase(this WebApplication app, ILoggerManager logger)
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

                // using var placeContext = scope.ServiceProvider.GetRequiredService<PlaceDbContext>();
                // try
                // {
                //     placeContext.Database.Migrate();
                //
                //     if (!await placeContext.Countries.AsNoTracking().AnyAsync())
                //     {
                //         var httpClientFactory = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>();
                //         var httpClient = httpClientFactory.CreateClient("geo-locations");
                //         httpClient.Timeout = TimeSpan.FromSeconds(30);
                //
                //         logger.LogInfo("Seeding data from API...");
                //
                //         var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                //         string countriesUri = "countries.json";
                //         string countriesResponse = await httpClient.GetStringAsync(countriesUri);
                //         var countries = JsonSerializer.Deserialize<List<Country>>(countriesResponse, options);
                //
                //         if (countries != null)
                //         {
                //             using var transaction = await placeContext.Database.BeginTransactionAsync();
                //
                //             placeContext.ChangeTracker.AutoDetectChangesEnabled = false;
                //
                //             int counter = 0;
                //
                //             foreach (var country in countries)
                //             {
                //                 var url = $"locations/{country.Code}.json";
                //                 bool success = await GeoParser.ProcessGeoData(httpClient, url, country, options);
                //
                //                 if (!success) {
                //                     logger.LogWarn($"Skipping {country.Name} ({country.Code}) — location data not found.");
                //                     continue;
                //                 }
                //
                //                 placeContext.Countries.Add(country);
                //                 counter++;
                //
                //                 logger.LogInfo($"Buffered {country.Name} for insertion...");
                //
                //                 if (counter % 20 == 0)
                //                 {
                //                     logger.LogInfo("Saving batch...");
                //                     await placeContext.SaveChangesAsync();
                //                     placeContext.ChangeTracker.Clear();
                //                 }
                //             }
                //
                //             await placeContext.SaveChangesAsync();
                //             await transaction.CommitAsync();
                //
                //             placeContext.ChangeTracker.AutoDetectChangesEnabled = true;
                //
                //             logger.LogInfo("Seeding successful.");
                //         }
                //     }
                //     else
                //     {
                //         logger.LogInfo("Database already seeded. Skipping API call.");
                //     }
                // }
                // catch (Exception ex)
                // {
                //     logger.LogError(ex.Message);
                //     throw;
                // }

            }
            return app;
        }
    }
}
