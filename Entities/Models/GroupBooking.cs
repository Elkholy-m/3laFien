using Entities.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class GroupBooking
    {
        public Guid GroupBookingId { get; set; }
        public DateTime? BookingDate { get; set; }
        public int NoOfGuests { get; set; }
        public BookingStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(Group))]
        public Guid GroupId { get; set; }

        [ForeignKey(nameof(Place))]
        public Guid PlaceId { get; set; }

        // Navigational Properties
        public Group? Group { get; set; }
        public Place? Place { get; set; }
    }
}
