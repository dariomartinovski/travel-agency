using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Enum;
using TravelAgencyApplication.Domain.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TravelAgencyApplication.Domain.Model
{
    public class TravelPackage : BaseEntity
    {
      public string? Title { get; set; }
      public string? Details { get; set; }
     public string? ImageUrl { get; set; }

     public double BasePrice { get; set; }
     public DateTime DepartureDate { get; set; }
     public DateTime ReturnDate { get; set; }

    public Guid DestinationId { get; set; }
    public Destination? Destination { get; set; }

    public int MaxCapacity { get; set; }

    public int CurrentCapacity { get; set; }
    public int Views {  get; set; }

    public Season Season { get; set; }
    public virtual ICollection<TravelPackageTag>? Tags { get; set; } = new List<TravelPackageTag>();
    public virtual ICollection<TravelPackageItinerary>? Itineraries { get; set; } = new List<TravelPackageItinerary>();
    public virtual ICollection<TravelPackageDepartureLocation>? DepartureLocations { get; set; } = new List<TravelPackageDepartureLocation>();
    public string? UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public TAUser? Guide { get; set; }
    public TransportType TransportType { get; set; }
    }
}
