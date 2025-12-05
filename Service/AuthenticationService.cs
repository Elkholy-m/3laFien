
using AutoMapper;
using Contracts;
using Entities.Models;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IRepositoryManager _repository;
        private readonly IConfiguration _config; // Needed for Google ClientId

        public AuthenticationService(
            UserManager<User> userManager,
            ITokenService tokenService,
            IRepositoryManager repository,
            IConfiguration config)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _repository = repository;
            _config = config;
        }

        // 1. REGISTER
        public async Task<IdentityResult> RegisterUserAsync(UserRegisterationModel model)
        {
            IdentityResult result = IdentityResult.Failed();

            await _repository.ExecuteInTransactionAsync(async () =>
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded) throw new Exception("User creation failed");

                await _userManager.AddToRoleAsync(user, "User");

                // Create Stub Visitor
                _repository.Visitor.CreateVisitor(user.Id, new Visitor
                {
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow,
                    About = "Write about yourself"
                });

                await _repository.SaveAsync();
            });

            return result;
        }

        // 2. LOGIN
        public async Task<AuthResponseDto?> LoginUserAsync(UserLoginModel model, bool isNew = false)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return null;

            return await GenerateAuthResponseAsync(user, isNew);
        }

        // 3. GOOGLE LOGIN
        public async Task<AuthResponseDto> HandleGoogleLoginAsync(GoogleLoginDto model)
        {
            // A. Validate Token
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { _config["Authentication:Google:ClientId"] },
                Clock = new SkewedClock(TimeSpan.FromMinutes(2))
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(model.idToken, settings);

            // B. Check if User Exists
            var user = await _userManager.FindByEmailAsync(payload.Email);
            bool isNew = false;

            // C. Create User + Visitor if new (In Transaction)
            if (user == null)
            {
                await _repository.ExecuteInTransactionAsync(async () =>
                {
                    user = new User
                    {
                        UserName = payload.Email,
                        Email = payload.Email,
                        EmailConfirmed = true,
                        FirstName = payload.Name,
                        LastName = payload.FamilyName
                    };

                    var result = await _userManager.CreateAsync(user);
                    if (!result.Succeeded) throw new Exception("Google User creation failed");

                    await _userManager.AddToRoleAsync(user, "User");

                    // Create Stub Visitor for Google User too!
                    _repository.Visitor.CreateVisitor(user.Id, new Visitor
                    {
                        UserId = user.Id,
                        CreatedAt = DateTime.UtcNow,
                        About = "Write about yourself"
                    });

                    await _repository.SaveAsync();
                });
                isNew = true;
            }

            return await GenerateAuthResponseAsync(user, isNew);
        }

        // Helper to generate the Token/Response
        private async Task<AuthResponseDto> GenerateAuthResponseAsync(User user, bool isNew)
        {
            // Find Visitor ID
            var visitor = await _repository.Visitor.GetVisitorByUserIdAsync(user.Id, trackChanges: false);

            // Create Token (Passes Visitor ID)
            var token = await _tokenService.CreateTokenAsync(user, visitor?.VisitorId);

            return new AuthResponseDto(token, isNew, user.Email!, user.FirstName!, user.LastName!);
        }
    }
}
