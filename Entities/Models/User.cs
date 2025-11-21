using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class User : IdentityUser<Guid>
    {
        [Required(ErrorMessage = "First name is required.")]
        public string? FirstName {  get; set; }
        public string? LastName {  get; set; }
        public DateTime CreatedAt {  get; set; }
        public string? ImageUrl {  get; set; }
        public bool IsDeleted { get; set; }

        // Navigational Properties
        public ICollection<SocialAccount>? SocialAccounts { get; set; }
        public ICollection<FavouritePlaces>? FavoritePlaces { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<GroupMember>? GroupMembers { get; set; }
        public ICollection<Group>? Groups { get; set; }
        public ICollection<PlaceOwner>? PlaceOwners { get; set; }

    }
}
