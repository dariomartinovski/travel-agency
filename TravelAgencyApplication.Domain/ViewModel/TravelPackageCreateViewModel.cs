using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Model;

namespace TravelAgencyApplication.Domain.ViewModel
{
    public class TravelPackageCreateViewModel
    {
        public TravelPackage TravelPackage { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Itinerary> Itineraries { get; set; }
        public List<DepartureLocation> DepartureLocations { get; set; }
    }

}
