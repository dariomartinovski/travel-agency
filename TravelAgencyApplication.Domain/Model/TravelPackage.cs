using System;
using System.Collections.Generic;
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

     public double BasePrice { get; set; }
     public DateTime DepartureDate { get; set; }
     public DateTime ReturnDate { get; set; }
    // number of days
    public int Duration {  get; set; }
    public Guid DestinationId { get; set; }
    public Destination? Destination { get; set; }

    public int MaxCapacity { get; set; }

    public int CurrentCapacity { get; set; }
    public int Views {  get; set; }

    public Season Season { get; set; }
    public virtual List<TravelPackageAccomodationImage>? TravelPackageAccomodationImages { get; set; }
    public virtual List<TravelPackageLocationImage>? TravelPackageLocationImages { get; set; }
    public virtual List<Tag>? Tags { get; set; }

    public virtual ICollection<TravelPackageItinerary>? Itineraries { get; set; }
    public virtual List<DepartureLocation>? DepartureLocations { get; set; }

    public string? UserId { get; set; }
    public TAUser? User { get; set; }
    public TransportType TransportType { get; set; }

    }
}
