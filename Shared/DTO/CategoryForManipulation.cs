using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class CategoryForManipulation
    {

        [Required(ErrorMessage = "Name is a required field.")]
        [MaxLength(50, ErrorMessage = "The max length for the Name is 50 characters.")]
        public string? Name { get; init; }

        [MaxLength(200, ErrorMessage = "The max length for the Description is 200 characters.")]
        public string? Description { get; init; }

    }
}
