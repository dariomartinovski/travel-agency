using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Details { get; set; }

        public double BasePrice { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public Guid DestinationId { get; set; }

        public int MaxCapacity { get; set; }

        public Season Season { get; set; }
        public List<Guid>? Tags { get; set; } = new List<Guid>();
        public List<Guid>? Itineraries { get; set; } = new List<Guid>();
        public List<Guid>? DepartureLocations { get; set; } = new List<Guid>();
        public string? UserId { get; set; }
        public TransportType TransportType { get; set; }
    }
}