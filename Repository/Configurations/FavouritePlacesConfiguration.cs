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
            builder.HasKey(x => new { x.VisitorId, x.PlaceId });
        }
    }
}
