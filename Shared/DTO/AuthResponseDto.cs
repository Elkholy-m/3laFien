using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public record AuthResponseDto(string Token, bool IsNewUser, string Email, string FirstName, string LastName);
}
