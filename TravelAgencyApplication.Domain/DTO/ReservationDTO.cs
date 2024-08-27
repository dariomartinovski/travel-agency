using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Enum;

namespace TravelAgencyApplication.Domain.DTO
{
    public class ReservationDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string TravelPackageTitle { get; set; }
        public IEnumerable<string> ItineraryTitles { get; set; }
        public int NumberOfPeople { get; set; }
        public bool HasPaid{ get; set; }
        public int TotalPrice { get; set; }
    }
}
