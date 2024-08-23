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
        //public virtual DbSet<TravelPackageDepartureLocation> TravelPackageDepartureLocations { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<TravelPackageItinerary>()
            //    .HasOne(tpi => tpi.TravelPackage)
            //    .WithMany(tp => tp.Itineraries)
            //    .HasForeignKey(tpi => tpi.TravelPackageId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<TravelPackageItinerary>()
            //    .HasOne(tpi => tpi.Itinerary)
            //    .WithMany(i => i.TravelPackages)
            //    .HasForeignKey(tpi => tpi.ItineraryId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<TravelPackage>()
            //    .HasMany(tp => tp.Itineraries)
            //    .WithOne(tpi => tpi.TravelPackage)
            //    .HasForeignKey(tpi => tpi.TravelPackageId)
            //    .OnDelete(DeleteBehavior.Cascade);



            //modelBuilder.Entity<TravelPackageDepartureLocation>()
            //    .HasOne(tpi => tpi.TravelPackage)
            //    .WithMany(tp => tp.DepartureLocations)
            //    .HasForeignKey(tpi => tpi.TravelPackageId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<TravelPackageDepartureLocation>()
            //    .HasOne(tpi => tpi.DepartureLocation)
            //    .WithMany(i => i.TravelPackages)
            //    .HasForeignKey(tpi => tpi.DepartureLocationId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<TravelPackage>()
            //    .HasMany(tp => tp.Tags)
            //    .WithOne(tpi => tpi.TravelPackage)
            //    .HasForeignKey(tpi => tpi.TravelPackageId)
            //    .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TravelPackageItinerary>()
                .HasOne(tpi => tpi.TravelPackage)
                .WithMany(tp => tp.Itineraries)
                .HasForeignKey(tpi => tpi.TravelPackageId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TravelPackageItinerary>()
                .HasOne(tpi => tpi.Itinerary)
                .WithMany(i => i.TravelPackages)
                .HasForeignKey(tpi => tpi.ItineraryId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<TravelPackageDepartureLocation>()
                .HasOne(tpdl => tpdl.TravelPackage)
                .WithMany(tp => tp.DepartureLocations)
                .HasForeignKey(tpdl => tpdl.TravelPackageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TravelPackageDepartureLocation>()
                .HasOne(tpdl => tpdl.DepartureLocation)
                .WithMany(dl => dl.TravelPackages)
                .HasForeignKey(tpdl => tpdl.DepartureLocationId)
                .OnDelete(DeleteBehavior.Restrict);


            // Define relationship for Tags and TravelPackage
            modelBuilder.Entity<TravelPackageTag>()
                .HasOne(tp => tp.TravelPackage)
                .WithMany(t => t.Tags)
                .HasForeignKey(t => t.TravelPackageId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TravelPackageTag>()
                .HasOne(tpdl => tpdl.Tag)
                .WithMany(dl => dl.TravelPackages)
                .HasForeignKey(tpdl => tpdl.TagId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
