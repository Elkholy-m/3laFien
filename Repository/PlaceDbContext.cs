using Entities.PlaceDBModels;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class PlaceDbContext(DbContextOptions<PlaceDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Country>()
            .Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Entity<State>()
            .Property(s => s.Id)
            .ValueGeneratedNever();

        builder.Entity<City>()
            .Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Entity<State>()
            .HasKey(st => new { st.CountryId, st.Id });

        builder.Entity<City>()
            .HasKey(ci => new { ci.CountryId, ci.StateId, ci.Id });

        builder.Entity<State>().
            HasOne(x => x.Country).
            WithMany(x => x.States).
            OnDelete(DeleteBehavior.Restrict);

        builder.Entity<City>()
            .HasOne(c => c.State)
            .WithMany(s => s.Cities)
            .HasForeignKey(c => new { c.CountryId, c.StateId })
            . OnDelete(DeleteBehavior.Restrict);
    }

    public DbSet<Country> Countries { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<City> Cities { get; set; }
}
