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
    internal class GruopMembersConfiguration : IEntityTypeConfiguration<GroupMember>
    {
        public void Configure(EntityTypeBuilder<GroupMember> builder)
        {
            builder.HasOne(fp => fp.User)
                .WithMany(u => u.GroupMembers)
                .HasForeignKey(fp => fp.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(fp => fp.Group)
                .WithMany(u => u.GroupMembers)
                .HasForeignKey(u => u.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasKey(x => new { x.GroupId, x.UserId });
        }
    }
}
