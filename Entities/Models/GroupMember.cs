using Entities.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class GroupMember : IHasJoinedAt
    {
        public bool IsAdmin { get; set; }
        public DateTime JoinedAt { get; set; }
        public MemberStatus Status { get; set; }

        [ForeignKey(nameof(Visitor))]
        public Guid VisitorId { get; set; }

        [ForeignKey(nameof(Group))]
        public Guid GroupId { get; set; }

        // Navigational Properties
        public Visitor? Visitor { get; set; }
        public Group? Group { get; set; }
    }
}
