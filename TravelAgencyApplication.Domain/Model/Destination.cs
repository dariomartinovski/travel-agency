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

    }
}
