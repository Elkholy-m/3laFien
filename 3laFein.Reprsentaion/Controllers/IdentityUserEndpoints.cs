using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service;
using Entities.Models;
using Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Service.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing;


namespace _3laFein.Reprsentaion.Controllers
{
    public static class IdentityUserEndpoints
    {

        [AllowAnonymous]
        public static async Task<IResult> CreateUser([FromBody]UserRegisterationModel
                userRegisterationModel, UserManager<User> userManager, ITokenService tokenService)
        {
            var existingUser = await userManager.FindByEmailAsync(userRegisterationModel.Email);
            if (existingUser != null)
            {
                return Results.BadRequest(new { errors = new[] { "The email already exists!" } });
            }

            User user = new User
            {
                Email = userRegisterationModel.Email,
                FirstName = userRegisterationModel.FirstName,
                LastName = userRegisterationModel.LastName,
                UserName = userRegisterationModel.Email,
            };
            var result = await userManager.CreateAsync(user, userRegisterationModel.Password);
            
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User");
                return await SignInUserAsync(userRegisterationModel.Email, userRegisterationModel.Password, userManager, tokenService);
            } else
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return Results.BadRequest(new { errors }); 
            }
         
        }

        [AllowAnonymous]
        public static async Task<IResult> SignInWithGoogle(
            GoogleLoginDto dto,
            HttpContext http,
            UserManager<User> userManager,
            ITokenService tokenService,
            IConfiguration config)
        {

            if (dto is null || string.IsNullOrWhiteSpace(dto.idToken))
                return Results.BadRequest("Google ID token is missing");

            // 2️⃣ Validate token with Google
            GoogleJsonWebSignature.Payload payload;
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(
                    dto.idToken,
                    new GoogleJsonWebSignature.ValidationSettings
                    {
                        Audience = new[] { config["Authentication:Google:ClientId"] },
                        Clock = new SkewedClock(TimeSpan.FromMinutes(2))
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return Results.BadRequest("Invalid Google token.");
            }

            // 3️⃣ Try to find user
            var user = await userManager.Users
                .FirstOrDefaultAsync(u => u.Email == payload.Email);

            var isNew = false;

            // 4️⃣ If not found, create a new user
            if (user == null)
            {
                user = new User
                {
                    UserName = payload.Email,
                    Email = payload.Email,
                    EmailConfirmed = true,
                    FirstName = payload.Name,
                    LastName = payload.FamilyName
                   
                };

                var result = await userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    return Results.BadRequest(result.Errors);
                }
                await userManager.AddToRoleAsync(user, "User");
                isNew = true;
            }

            // 5️⃣ Issue JWT
            var jwt = await tokenService.CreateTokenAsync(user);

            return Results.Ok(new
            {
                token = jwt,
                isNewUser = isNew,
                email = user.Email,
                firstName = user.FirstName,
                lastName = user.LastName
            });
        }

        [AllowAnonymous]
        public static async Task<IResult> SignIn(
        [FromBody] UserLoginModel userLoginModel,
        UserManager<User> userManager,
        ITokenService tokenService)
        {
            return await SignInUserAsync(userLoginModel.Email, userLoginModel.Password, userManager, tokenService);
        }

        public static async Task<IResult> SignInUserAsync(string email, string password, UserManager<User> userManager, ITokenService tokenService)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null || !await userManager.CheckPasswordAsync(user, password))
                return Results.BadRequest("Invalid email or password.");

            var jwt = await tokenService.CreateTokenAsync(user);

            return Results.Ok(new { token = jwt });

        }


    }
}
