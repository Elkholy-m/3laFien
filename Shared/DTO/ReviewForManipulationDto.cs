using Entities.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class ReviewForManipulationDto
    {
        [MaxLength(250, ErrorMessage = "The max length for the Comment is 250 characters.")]
        public string? Comment { get; init; }

        [Required]
        public Ratings Rating { get; init; }
    }
}
