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
    internal class PlaceConfiguration : IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> builder)
        {
            builder.Property(x => x.Price).HasPrecision(18, 2);
            builder.Property(x => x.DiscountPercentage).HasPrecision(18, 2);

            builder.HasData(
                new Place
                {
                    PlaceId = new Guid("615d0417-1d1b-4541-968c-5bb9927e764a"),
                    Name = "Pyramids",
                    Description = "Pyramids of giza",
                    Country = "Egypt",
                    City = "Giza",
                    Street = "N/A",
                    Latitude = 29.9792f,
                    Longitude = 31.1343f,
                    Price = 200,
                    Rate = 4.3f,
                    TotalReviews = 10,
                    DiscountPercentage = 2,
                    CreatedAt = new DateTime(2000, 4, 4),
                    IsDeleted = false,
                    CategoryId = 1
                },
                new Place
                {
                    PlaceId = new Guid("7580dc3b-88c6-4344-9627-c8941a1959a1"),
                    Name = "Hotel",
                    Description = "book to stay and do Islamic rituals",
                    Country = "KSA",
                    City = "Mecca",
                    Street = "N/A",
                    Latitude = 21.4241f,
                    Longitude = 39.8173f,
                    Price = 300,
                    Rate = 5f,
                    TotalReviews = 100,
                    DiscountPercentage = 4,
                    CreatedAt = new DateTime(1990, 4, 4),
                    IsDeleted = false,
                    CategoryId = 2
                }
            );
        }
    }
}
