using Entities.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public CategoriesName Name { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }

        // Navigational Properties
        public ICollection<Place>? Places { get; set; }
    }
}
