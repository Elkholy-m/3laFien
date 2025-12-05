using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class FavouritePlaceForCreationDto
    {
        [Required(ErrorMessage = "Place ID is a requried field.")]
        public Guid PlaceId { get; set; }
    }
}
