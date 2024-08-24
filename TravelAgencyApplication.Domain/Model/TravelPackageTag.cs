using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyApplication.Domain.Model
{
    public class TravelPackageTag: BaseEntity
    {
        public TravelPackage? TravelPackage { get; set; }
        public Guid TravelPackageId { get; set; }
        public Tag? Tag { get; set; }
        public Guid TagId { get; set; }
    }
}
