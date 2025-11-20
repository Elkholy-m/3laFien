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
    internal class FavouritePlacesConfiguration : IEntityTypeConfiguration<FavouritePlaces>
    {
        public void Configure(EntityTypeBuilder<FavouritePlaces> builder)
        {
            builder.HasOne(fp => fp.User)
                .WithMany(u => u.FavoritePlaces)
                .HasForeignKey(fp => fp.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(fp => fp.Place)
                .WithMany(u => u.FavouritePlaces)
                .HasForeignKey(fp => fp.PlaceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasKey(x => new { x.UserId, x.PlaceId });
        }
    }
}
