using Entities.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public record class SocialAccountDto (Guid AccountId, Platforms Platform, string PlatformUsername, string Url) { }
}
