using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Enum;
using TravelAgencyApplication.Domain.Identity;
using TravelAgencyApplication.Domain.Model;

namespace TravelAgencyApplication.Domain.DTO
{
    public class TravelPackageDTO
    {
        public string? Title { get; set; }
        public string? Details { get; set; }

        public double BasePrice { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public Guid DestinationId { get; set; }
        public int MaxCapacity { get; set; }
        public int CurrentCapacity { get; set; } = 0;
        public int Views { get; set; } = 0;

        public string Season { get; set; }

        public string? UserId { get; set; }
        public string TransportType { get; set; }

    }
}