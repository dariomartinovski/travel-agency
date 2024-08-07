using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyApplication.Domain.Model
{
    public class TravelPackageItinerary: BaseEntity
    {
        public TravelPackage? TravelPackage { get; set; }

        public Guid TravelPackageId { get; set; }

        public Itinerary? Itinerary { get; set; }

        public Guid ItineraryId { get; set; }
    }
}
