using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
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
            base.OnModelCreating(modelBuilder);

            // Loop through all relationships in the model
            foreach (var foreignKey in modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

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
