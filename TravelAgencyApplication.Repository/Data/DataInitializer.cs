using TravelAgencyApplication.Domain.Model;
using TravelAgencyApplication.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using TravelAgencyApplication.Domain.Enum;
using System;
using TravelAgencyApplication.Repository.Interface;

namespace TravelAgencyApplication.Repository.Data
{
    public static class DataInitializer
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            //// Seed Countries
            //var country1 = new Country { Id = Guid.NewGuid(), Name = "USA" };
            //var country2 = new Country { Id = Guid.NewGuid(), Name = "France" };

            //modelBuilder.Entity<Country>().HasData(country1, country2);

            //// Seed Cities
            //var city1 = new City { Id = Guid.NewGuid(), Name = "New York" };
            //var city2 = new City { Id = Guid.NewGuid(), Name = "Paris" };

            //modelBuilder.Entity<City>().HasData(city1, city2);

            //// Seed Destinations
            //var destination1 = new Destination { Id = Guid.NewGuid(), CountryId = country1.Id, CityId = city1.Id };
            //var destination2 = new Destination { Id = Guid.NewGuid(), CountryId = country2.Id, CityId = city2.Id };

            //modelBuilder.Entity<Destination>().HasData(destination1, destination2);

            //// Seed Tags
            //var tag1 = new Tag { Id = Guid.NewGuid(), Name = "Adventure" };
            //var tag2 = new Tag { Id = Guid.NewGuid(), Name = "Luxury" };

            //modelBuilder.Entity<Tag>().HasData(tag1, tag2);

            //// Seed Users (Guides)
            //var guide1 = new TAUser { Id = "user1", UserName = "Guide1", Email = "guide1@example.com" };
            //var guide2 = new TAUser { Id = "user2", UserName = "Guide2", Email = "guide2@example.com" };

            //modelBuilder.Entity<TAUser>().HasData(guide1, guide2);

            //// Seed TravelPackages
            //var travelPackage1 = new TravelPackage
            //{
            //    Id = Guid.NewGuid(),
            //    Title = "New York Adventure",
            //    Details = "An exciting adventure in New York City.",
            //    BasePrice = 500,
            //    DepartureDate = DateTime.Now.AddDays(20),
            //    ReturnDate = DateTime.Now.AddDays(27),
            //    DestinationId = destination1.Id,
            //    MaxCapacity = 20,
            //    CurrentCapacity = 10,
            //    Views = 100,
            //    Season = Season.SUMMER,
            //    UserId = guide1.Id,
            //    TransportType = TransportType.AIRPLANE,
            //};

            //var travelPackage2 = new TravelPackage
            //{
            //    Id = Guid.NewGuid(),
            //    Title = "Paris Luxury Experience",
            //    Details = "Experience the luxury of Paris.",
            //    BasePrice = 1500,
            //    DepartureDate = DateTime.Now.AddDays(25),
            //    ReturnDate = DateTime.Now.AddDays(32),
            //    DestinationId = destination2.Id,
            //    MaxCapacity = 15,
            //    CurrentCapacity = 5,
            //    Views = 200,
            //    Season = Season.SPRING,
            //    UserId = guide2.Id,
            //    TransportType = TransportType.AIRPLANE,
            //};

            //modelBuilder.Entity<TravelPackage>().HasData(travelPackage1, travelPackage2);

            //// Seed DepartureLocations (no TravelPackageId)
            //var departureLocation1 = new DepartureLocation
            //{
            //    Id = Guid.NewGuid(),
            //    CityId = city1.Id,
            //    Location = "JFK Airport",
            //    DepartureTime = new TimeOnly(8, 30)
            //};

            //var departureLocation2 = new DepartureLocation
            //{
            //    Id = Guid.NewGuid(),
            //    CityId = city2.Id,
            //    Location = "CDG Airport",
            //    DepartureTime = new TimeOnly(9, 45)
            //};

            //modelBuilder.Entity<DepartureLocation>().HasData(departureLocation1, departureLocation2);

            //// Seed Itineraries
            //var itinerary1 = new Itinerary
            //{
            //    Id = Guid.NewGuid(),
            //    Title = "Day Trip to Statue of Liberty",
            //    Details = "Visit the iconic Statue of Liberty.",
            //    Price = 50,
            //    Date = DateTime.Now.AddDays(10),
            //    DestinationId = destination1.Id
            //};

            //var itinerary2 = new Itinerary
            //{
            //    Id = Guid.NewGuid(),
            //    Title = "Louvre Museum Tour",
            //    Details = "Explore the famous Louvre Museum.",
            //    Price = 100,
            //    Date = DateTime.Now.AddDays(15),
            //    DestinationId = destination2.Id
            //};

            //modelBuilder.Entity<Itinerary>().HasData(itinerary1, itinerary2);

            //// Seed TravelPackageItineraries
            //var travelPackageItinerary1 = new TravelPackageItinerary
            //{
            //    Id = Guid.NewGuid(),
            //    TravelPackageId = travelPackage1.Id,
            //    ItineraryId = itinerary1.Id
            //};

            //var travelPackageItinerary2 = new TravelPackageItinerary
            //{
            //    Id = Guid.NewGuid(),
            //    TravelPackageId = travelPackage2.Id,
            //    ItineraryId = itinerary2.Id
            //};

            //modelBuilder.Entity<TravelPackageItinerary>().HasData(travelPackageItinerary1, travelPackageItinerary2);
        }
    }
}
