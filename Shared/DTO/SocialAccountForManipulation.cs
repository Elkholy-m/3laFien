using Entities.Models.Enums;
using Shared.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public abstract record class SocialAccountForManipulation
    {
        [EnumDataType(typeof(Platforms), ErrorMessage = "Invalid Platform.")]
        public Platforms Platform { get; init; }

        [Required(ErrorMessage = "The url is a required field.")]
        [PlatformValidation]
        public string? Url { get; init; }
    }
}
