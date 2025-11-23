using Entities.Models.Enums;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shared.CustomAttributes
{
    public class PlatformValidationAttribute  : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var dto = (SocialAccountForManipulation)validationContext.ObjectInstance!;
            var url = value as string;

            if (string.IsNullOrWhiteSpace(url))
                return new ValidationResult("URL is required.");

            bool isValid = dto.Platform switch
            {
                Platforms.X => Regex.IsMatch(url, @"^https://twitter\.com/[A-Za-z0-9_]{1,15}$"),
                Platforms.Facebook => Regex.IsMatch(url, @"^https://(www\.)?facebook\.com/[A-Za-z0-9\.]{1,50}$"),
                Platforms.Instagram => Regex.IsMatch(url, @"^https://(www\.)?instagram\.com/[A-Za-z0-9_.]{1,30}$"),
                Platforms.LinkedIn => Regex.IsMatch(url, @"^https://(www\.)?linkedin\.com/in/[A-Za-z0-9_-]{1,100}$"),
                Platforms.Threads => Regex.IsMatch(url, @"^https://www\.threads\.net/@[A-Za-z0-9_.]{1,30}$"),
                _ => false
            };

            return isValid ? ValidationResult.Success : new ValidationResult("URL does not match the selected platform.");
        }
    }
}
