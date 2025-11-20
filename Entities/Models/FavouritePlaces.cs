using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class FavouritePlaces
    {
        public DateTime AddedAt { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(Place))]
        public Guid PlaceId { get; set; }

        // Navigational Properties
        public User? User { get; set; }
        public Place? Place { get; set; }

    }
}
