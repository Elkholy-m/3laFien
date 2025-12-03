using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class PlaceForManipulation
    {
        [Required(ErrorMessage = "First name is a required field.")]
        [MaxLength(50, ErrorMessage = "The max length for the About is 100 characters.")]
        public string? Name { get; init; }

        [Required(ErrorMessage = "Category Is Required.")]
        public int CategoryId { get; set; }

        [MaxLength(250, ErrorMessage = "The max length for the Experience is 30 characters.")]
        public string? Description { get; init; }


        [MaxLength(50, ErrorMessage = "The max length for the Country is 50 characters.")]
        public string? Country { get; init; }

        [MaxLength(50, ErrorMessage = "The max length for the City is 50 characters.")]
        public string? City { get; init; }

        [MaxLength(150, ErrorMessage = "The max length for the Street is 50 characters.")]
        public string? Street { get; init; }

        // Longitude must be between -180.0 and 180.0
        [Range(-180.0, 180.0, ErrorMessage = "Longitude must be between -180 and 180.")]
        public double? Longitude { get; init; }

        // Latitude must be between -90.0 and 90.0
        [Range(-90.0, 90.0, ErrorMessage = "Latitude must be between -90 and 90.")]
        public double? Latitude { get; init; }

        // Price must be a positive value (0.01 or greater).
        // Note: Range attribute takes doubles for min/max, so we cast decimal.MaxValue.
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal? Price { get; init; }

        // DiscountPercentage must be between 0 and 100.
        [Range(0.0, 100.0, ErrorMessage = "Discount Percentage must be between 0 and 100.")]
        public decimal? DiscountPercentage { get; init; }


        }
}
