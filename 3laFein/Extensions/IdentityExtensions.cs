using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Entities.Models;
using _3laFein.Reprsentaion.Controllers;
using Entities.Seed;
using Google;
using Microsoft.EntityFrameworkCore;

namespace _3laFein.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityHandlersAndStores(this IServiceCollection services)
        {
            services.AddIdentityApiEndpoints<User>()
                .AddRoles<Role>()
                .AddEntityFrameworkStores<RepositoryContext>()
                .AddDefaultTokenProviders();
            return services;
        }

        public static IServiceCollection ConfigureIdentityOptions(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
            });

            return services;
        }

        public static IServiceCollection AddIdentityAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(configuration["AppSettings:JWTSecret"]!)
                    ),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            })
            .AddGoogle(options =>
            {
                options.ClientId = configuration["Authentication:Google:ClientId"]!;
                options.ClientSecret = configuration["Authentication:Google:ClientSecret"]!;
            });

            services.AddAuthorization(options =>
            {
            // todo: uncomment this line for the production 
            // suppress the authentication for eazy test end points
                // options.FallbackPolicy = new AuthorizationPolicyBuilder()
                //     .RequireAuthenticatedUser()
                //     .Build();
            });

            return services;
        }

        public static WebApplication AddIdentityAuthMiddlewares(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }

        public static IEndpointRouteBuilder MapIdentityUserEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("auth/signup", IdentityUserEndpoints.CreateUser);

            app.MapPost("auth/signin", IdentityUserEndpoints.SignIn);

            return app;
        }

        public static IEndpointRouteBuilder MapGoogleAuthEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("auth/google", IdentityUserEndpoints.SignInWithGoogle);
            return app;
        }

        public static async Task SeedIdentityAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<RepositoryContext>();
            await db.Database.MigrateAsync();

            await IdentitySeeder.SeedRolesAndAdminAsync(scope.ServiceProvider);
        }
    }
}
