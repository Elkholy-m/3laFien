using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryContext :
        IdentityDbContext<
            User,
            Role,
            Guid,
            IdentityUserClaim<Guid>,
            IdentityUserRole<Guid>,
            IdentityUserLogin<Guid>,
            IdentityRoleClaim<Guid>,
            IdentityUserToken<Guid>>
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FavouritePlacesConfiguration());
            modelBuilder.ApplyConfiguration(new GruopMembersConfiguration());
            modelBuilder.ApplyConfiguration(new PlaceConfiguration());
            modelBuilder.ApplyConfiguration(new GruopConfiguration());
            modelBuilder.ApplyConfiguration(new PlaceOwnerConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            base.OnModelCreating(modelBuilder);

            // Make all the relations restricted behaviour instead of cascaded
            foreach (var foreignKey in modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        // Auditable logic for CreatedAt, AddedAt, JoinedAt ...etc
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach(var entry in ChangeTracker.Entries())
            {
                var entity = entry.Entity;
                var state = entry.State;

                if (state == EntityState.Added)
                {
                    if (entity is IHasAddedAt addedAt)
                        addedAt.AddedAt = DateTime.UtcNow;

                    if (entity is IHasCreatedAt createdAt)
                        createdAt.CreatedAt = DateTime.UtcNow;

                    if (entity is IHasJoinedAt joinedAt)
                        joinedAt.JoinedAt = DateTime.UtcNow;
                }

                if (entity is ISoftDelete softDeletable && state == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    softDeletable.IsDeleted = true;
                    softDeletable.DeletedAt = DateTime.UtcNow;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<SocialAccount> SocialAccounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupBooking> GroupsBooking { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<PlaceImage> PlaceImages { get; set; }
        public DbSet<PlaceSchedule> PlaceSchedules { get; set; }
        public DbSet<Booking> UsersBooking { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<FavouritePlaces> FavoritePlaces { get; set; }
        public DbSet<PlaceOwner> PlaceOwners { get; set; }
    }
}
