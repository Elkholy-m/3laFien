using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class User : IdentityUser<Guid>, ISoftDelete
    {
        [Required(ErrorMessage = "First name is required.")]
        public string? FirstName {  get; set; }
        public string? LastName {  get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; }

        // Navigational Property
        public Visitor? Visitor { get; set; }
    }
}
