using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category
                {
                    CategoryId = 1,
                    Name = Entities.Models.Enums.CategoriesName.Resturant,
                    Description = "Test",
                    IsDeleted = false
                },
                new Category
                {
                    CategoryId = 2,
                    Name = Entities.Models.Enums.CategoriesName.Hotel,
                    Description = "Test",
                    IsDeleted = false
                }
            );
        }
    }
}
