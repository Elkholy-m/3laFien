using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Place : IHasCreatedAt, ISoftDelete
    {
        [Key]
        public Guid PlaceId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public Point? Location { get; set; }
        public decimal Price { get; set; }
        public float Rate { get; set; }
        public int TotalReviews { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        [NotMapped]
        public decimal DiscountedPrice
        {
            get
            {
                if (DiscountPercentage <= 0) return Price;
                return Price - (Price * (DiscountPercentage / 100m));
            }
        }

        // Navigational Properties
        public Category? Category { get; set; }
        public ICollection<FavouritePlaces>? FavouritePlaces { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<Group>? Groups { get; set; }
        public ICollection<GroupBooking>? GroupBooking { get; set; }
        public ICollection<PlaceImage>? PlaceImages { get; set; }
        public ICollection<PlaceSchedule>? PlaceSchedules { get; set; }
        public ICollection<PlaceOwner>? PlaceOwners { get; set; }

    }
}
