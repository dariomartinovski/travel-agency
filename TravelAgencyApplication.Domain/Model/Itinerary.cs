using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Enum;
using TravelAgencyApplication.Domain.Identity;

namespace TravelAgencyApplication.Domain.Model
{

    public class Itinerary : BaseEntity
    {
        public string Title { get; set; }
        public string Details { get; set; }

        public double Price { get; set; }

        public DateTime Date { get; set; }

        public Destination? Destination { get; set; }
        public Guid DestinationId { get; set; }

        public virtual ICollection<TravelPackageItinerary>? TravelPackages { get; set; }
    }
}
