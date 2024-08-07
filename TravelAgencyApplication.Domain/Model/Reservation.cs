using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Enum;
using TravelAgencyApplication.Domain.Identity;

namespace TravelAgencyApplication.Domain.Model
{
    public class Reservation : BaseEntity
    {
        public string UserId { get; set; }

        public TAUser? User { get; set; }

        public TravelPackage? TravelPackage { get; set; }

        public Guid TravelPackageId { get; set; }

        public DateTime Date { get; set; }    

        public int NumberOfPeople { get; set; }

        public int TotalPrice { get; set; }

        public ReservationStatus ReservationStatus { get; set; }

        public bool HasPaid { get; set; }

    }
}
