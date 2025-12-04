using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Service.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing;
using Shared.DTO;


namespace _3laFein.Reprsentaion.Controllers
{
    public static class IdentityUserEndpoints
    {
        // REGISTER
        [AllowAnonymous]
        public static async Task<IResult> CreateUser(
            [FromBody] UserRegisterationModel model,
            IServiceManager service) // Inject ServiceManager
        {
            try
            {
                // 1. Register (Creation + Visitor + Transaction)
                var result = await service.AuthenticationService.RegisterUserAsync(model);

                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description).ToList();
                    return Results.BadRequest(new { errors });
                }

                // 2. If success, Auto-Login to return the token
                var loginResult = await service.AuthenticationService.LoginUserAsync(new UserLoginModel
                {
                    Email = model.Email,
                    Password = model.Password
                }, true);

                return Results.Ok(loginResult);
            }
            catch (Exception ex)
            {
                // This catches the transaction rollback exception
                return Results.BadRequest(new { errors = new[] { ex.Message } });
            }
        }

        // LOGIN
        [AllowAnonymous]
        public static async Task<IResult> SignIn(
            [FromBody] UserLoginModel model,
            IServiceManager service)
        {
            var result = await service.AuthenticationService.LoginUserAsync(model);

            if (result == null)
                return Results.BadRequest("Invalid email or password.");

            return Results.Ok(result);
        }

        // GOOGLE LOGIN
        [AllowAnonymous]
        public static async Task<IResult> SignInWithGoogle(
            [FromBody] GoogleLoginDto dto,
            IServiceManager service)
        {
            if (dto is null || string.IsNullOrWhiteSpace(dto.idToken))
                return Results.BadRequest("Google ID token is missing");

            try
            {
                var result = await service.AuthenticationService.HandleGoogleLoginAsync(dto);
                return Results.Ok(result);
            }
            catch (InvalidJwtException)
            {
                return Results.BadRequest("Invalid Google token.");
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { errors = new[] { ex.Message } });
            }
        }
    }
}
