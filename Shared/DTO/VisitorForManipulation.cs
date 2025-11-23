using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public abstract record class VisitorForManipulation
    {
        [Required(ErrorMessage = "First name is a required field.")]
        [MaxLength(100, ErrorMessage = "The max length for the About is 100 characters.")]
        public string? About { get; init; }

        [MaxLength(30, ErrorMessage = "The max length for the Experience is 30 characters.")]
        public string? Experience { get; init; }


        [MaxLength(30, ErrorMessage = "The max length for the Skills is 30 characters.")]
        public string? Skills { get; init; }


        [MaxLength(30, ErrorMessage = "The max length for the TourStyle is 30 characters.")]
        public string? TourStyle { get; init; }


        [MaxLength(30, ErrorMessage = "The max length for the Interests is 30 characters.")]
        public string? Interests { get; init; }
    }
}
