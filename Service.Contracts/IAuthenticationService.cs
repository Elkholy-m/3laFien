using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IAuthenticationService
    {
        // Normal Registration
        Task<IdentityResult> RegisterUserAsync(UserRegisterationModel model);

        // Normal Login
        Task<AuthResponseDto?> LoginUserAsync(UserLoginModel model, bool isNew = false);

        // Google Login (Handles validation + registration + login)
        Task<AuthResponseDto> HandleGoogleLoginAsync(GoogleLoginDto model);
    }
}
