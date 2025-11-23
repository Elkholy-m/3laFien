using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Group : IHasCreatedAt, ISoftDelete
    {
        public Guid GroupId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; }

        [ForeignKey(nameof(Visitor))]
        public Guid VisitorId { get; set; }

        [ForeignKey(nameof(Place))]
        public Guid PlaceId { get; set; }


        // Navigational Properties
        public Visitor? Visitor { get; set; }
        public Place? Place { get; set; }
        public ICollection<GroupMember>? GroupMembers { get; set; }
        public ICollection<GroupBooking>? GroupBookings { get; set; }

    }
}
