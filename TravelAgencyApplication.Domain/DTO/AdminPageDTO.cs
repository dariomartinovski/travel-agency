using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Identity;
using TravelAgencyApplication.Domain.Model;

namespace TravelAgencyApplication.Domain.DTO
{
    public class AdminPageDTO
    {
        public List<City> Cities { get; set; }
        public List<Country> Countries { get; set; }
        public List<DepartureLocation> DepartureLocations { get; set; }
        public List<Destination> Destinations { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Itinerary> Itineraries { get; set; }
        public List<TravelPackage> TravelPackages { get; set; }
        public List<TAUser> Users { get; set; }

    }
}
