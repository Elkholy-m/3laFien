using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class PlaceAddress
    {
        [Key]
        public Guid AddressId { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }

        [ForeignKey(nameof(Place))]
        public Guid PlaceId { get; set; }

        // Navigational Properties
        public Place? Place { get; set; }

    }
}
