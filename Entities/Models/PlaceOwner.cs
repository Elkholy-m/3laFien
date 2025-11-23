using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class PlaceOwner : IHasAddedAt
    {
        public DateTime AddedAt { get; set; }

        [ForeignKey(nameof(Visitor))]
        public Guid VisitorId { get; set; }

        [ForeignKey(nameof(Place))]
        public Guid PlaceId { get; set; }

        //Navigational Properties
        public Visitor? Visitor { get; set; }
        public Place? Place { get; set; }
    }
}
