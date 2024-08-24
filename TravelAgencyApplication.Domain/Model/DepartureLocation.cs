using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyApplication.Domain.Model
{
    public class DepartureLocation : BaseEntity
    {
        public City? City { get; set; }

        public Guid CityId { get; set; }
        public string? Location { get; set; }
        public TimeOnly DepartureTime { get; set; }
        public ICollection<TravelPackageDepartureLocation> TravelPackages { get; set; } = new List<TravelPackageDepartureLocation>();
        public override string ToString()
        {
            return $"{City?.Name ?? "Unknown City"}, {Location ?? "Unknown Location"} at {DepartureTime:hh\\:mm tt}";
        }
    }
}
