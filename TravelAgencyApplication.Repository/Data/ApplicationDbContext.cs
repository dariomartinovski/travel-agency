using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelAgencyApplication.Domain.Identity;
using TravelAgencyApplication.Domain.Model;
using TravelAgencyApplication.Repository.Data;

namespace TravelAgencyApplication.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<TAUser>
    {
        public virtual DbSet<TravelPackage> TravelPackages { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Itinerary> Itineraries { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<DepartureLocation> DeparatureLocations { get; set; }
        public virtual DbSet<Destination> Destinations { get; set; }
        public virtual DbSet<TravelPackageTag> TravelPackageTags { get; set; }
        public virtual DbSet<TravelPackageDepartureLocation> TravelPackageDepartureLocations { get; set; }
        public virtual DbSet<TravelPackageItinerary> TravelPackageItineraries { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure TravelPackage and related entities without cascade delete
            modelBuilder.Entity<TravelPackage>()
                .HasMany(tp => tp.DepartureLocations)
                .WithOne(tpl => tpl.TravelPackage)
                .HasForeignKey(tpl => tpl.TravelPackageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TravelPackage>()
                .HasMany(tp => tp.Tags)
                .WithOne(tpt => tpt.TravelPackage)
                .HasForeignKey(tpt => tpt.TravelPackageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TravelPackage>()
                .HasMany(tp => tp.Itineraries)
                .WithOne(tpi => tpi.TravelPackage)
                .HasForeignKey(tpi => tpi.TravelPackageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DepartureLocation>()
                .HasMany(dl => dl.TravelPackages)
                .WithOne(tpl => tpl.DepartureLocation)
                .HasForeignKey(tpl => tpl.DepartureLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tag>()
                .HasMany(t => t.TravelPackages)
                .WithOne(tpt => tpt.Tag)
                .HasForeignKey(tpt => tpt.TagId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Itinerary>()
                .HasMany(i => i.TravelPackages)
                .WithOne(tpi => tpi.Itinerary)
                .HasForeignKey(tpi => tpi.ItineraryId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
