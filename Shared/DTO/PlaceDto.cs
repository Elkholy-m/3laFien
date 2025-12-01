using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public record PlaceDto
    {
        public Guid PlaceId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string Country { get; init; }
        public string City { get; init; }
        public string Street { get; init; }
        public double Longitude { get; init; }
        public double Latitude { get; init; }
        public decimal Price { get; init; }
        public decimal DiscountPercentage { get; init; }
        public decimal DiscountedPrice { get; init; }
        public int TotalReviews { get; init; }
        public float Rate { get; init; }
    }
}
