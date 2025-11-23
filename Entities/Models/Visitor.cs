using Org.BouncyCastle.Math.EC.Multiplier;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Visitor : ISoftDelete, IHasCreatedAt
    {
        [Key]
        public Guid VisitorId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? About { get; set; }
        public string? Experience { get; set; }
        public string? Skills { get; set; }
        public string? TourStyle { get; set; }
        public string? Interests { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        // Navigational Properties
        public User? User { get; set; }
        public ICollection<SocialAccount>? SocialAccounts { get; set; }
        public ICollection<FavouritePlaces>? FavoritePlaces { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<GroupMember>? GroupMembers { get; set; }
        public ICollection<Group>? Groups { get; set; }
        public ICollection<PlaceOwner>? PlaceOwners { get; set; }
    }
}
