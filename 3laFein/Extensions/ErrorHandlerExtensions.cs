using Entities;
using Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace _3laFein.Extensions
{
    public static class ErrorHandlerExtensions
    {
        public static void HandleExceptions(this WebApplication app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var errorFeatures = context.Features.Get<IExceptionHandlerFeature>();
                    if (errorFeatures != null)
                    {
                        context.Response.StatusCode = errorFeatures.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            BadRequestException => StatusCodes.Status400BadRequest,
                            _ => StatusCodes.Status500InternalServerError
                        };

                        await context.Response.WriteAsync(new Error()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = errorFeatures.Error.Message
                        }.ToString());
                    }
                });
            });
        }
    }
}
