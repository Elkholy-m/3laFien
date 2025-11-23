using Entities.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Entities
{
    public static class ExtractUrlHelper
    {
        public static string ExtractUsername(Platforms platform, string url)
        {
            return platform switch
            {
                Platforms.Facebook => Regex.Match(url, @"facebook\.com\/(?<u>[A-Za-z0-9](?:[A-Za-z0-9\.]{3,48}[A-Za-z0-9])?)$").Groups["u"].Value,
                Platforms.X => Regex.Match(url, @"twitter\.com\/(?<u>[A-Za-z0-9_]{1,15})$").Groups["u"].Value,
                Platforms.Instagram => Regex.Match(url, @"instagram\.com\/(?<u>[A-Za-z0-9_.]+)\/?$").Groups["u"].Value,
                Platforms.LinkedIn => Regex.Match(url, @"linkedin\.com\/in\/(?<u>[A-Za-z0-9\-_]+)\/?$").Groups["u"].Value,
                Platforms.Threads => Regex.Match(url, @"threads\.net\/@(?<u>[A-Za-z0-9_.]+)$").Groups["u"].Value,
                _ => string.Empty
            };
        }
    }
}
