using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyApplication.Domain.Model
{
    public class TravelPackageDepartureLocation : BaseEntity
    {
        public TravelPackage TravelPackage { get; set; }
        public Guid TravelPackageId { get; set; }
        public DepartureLocation DepartureLocation { get; set; }
        public Guid DepartureLocationId { get; set; }
    }
}
