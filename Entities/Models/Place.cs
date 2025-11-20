using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Place
    {
        [Key]
        public Guid PlaceId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public decimal Price { get; set; }
        public float Rate { get; set; }
        public int TotalReviews { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        // Navigational Properties
        public User? User { get; set; }
        public Category? Category { get; set; }
        public ICollection<FavouritePlaces>? FavouritePlaces { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<Group>? Groups { get; set; }
        public ICollection<GroupBooking>? GroupBooking { get; set; }
        public ICollection<PlaceAddress>? PlaceAddresses { get; set; }
        public ICollection<PlaceImage>? PlaceImages { get; set; }
        public ICollection<PlaceSchedule>? PlaceSchedules { get; set; }

    }
}
