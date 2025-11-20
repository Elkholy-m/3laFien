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
    public class Review
    {
        [Key]
        public Guid ReviewId { get; set; }
        public Ratings Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedAt { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(Place))]
        public Guid PlaceId { get; set; }

        // Navigational Properties
        public User? User { get; set; }
        public Place? Place { get; set; }
    }
}
