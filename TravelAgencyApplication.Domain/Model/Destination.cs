using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyApplication.Domain.Model
{
    public class Destination : BaseEntity
    {
        public Country? Country { get; set; }
        public Guid CountryId { get; set; }
        public Guid CityId { get; set; }
        public City? City { get; set; }

        public override string ToString()
        {
            // Ensure that Country and City are not null before accessing their Name properties
            string countryName = Country?.Name ?? "Unknown Country";
            string cityName = City?.Name ?? "Unknown City";

            // Format the output string
            return $"{cityName}, {countryName}";
        }
    }
}
