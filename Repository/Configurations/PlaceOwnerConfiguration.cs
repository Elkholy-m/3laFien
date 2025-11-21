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
    public class PlaceOwnerConfiguration : IEntityTypeConfiguration<PlaceOwner>
    {
        public void Configure(EntityTypeBuilder<PlaceOwner> builder)
        {
            builder.HasKey(x => new { x.PlaceId, x.OwnerId });
        }
    }
}
