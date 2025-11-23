using Entities.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class SocialAccount
    {
        [Key]
        public Guid AccountId { get; set; }

        public Platforms Platform { get; set; }
        public string? Url { get; set; }

        [NotMapped]
        public string? PlatformUsername => ExtractUrlHelper.ExtractUsername(Platform, Url!);

        [ForeignKey(nameof(Visitor))]
        public Guid VisitorId { get; set; }

        // Navigational Property
        public Visitor? Visitor { get; set; }
    }
}
