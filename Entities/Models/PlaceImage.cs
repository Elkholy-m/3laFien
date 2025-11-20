using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class PlaceImage
    {
        [Key]
        public Guid ImageId { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsMain { get; set; }

        [ForeignKey(nameof(Place))]
        public Guid PlaceId { get; set; }

        // Navigational Properties
        public Place? Place { get; set; }
    }
}
