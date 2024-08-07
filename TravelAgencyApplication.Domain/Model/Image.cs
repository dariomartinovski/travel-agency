using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyApplication.Domain.Model
{
    public class Image : BaseEntity
    {
        public byte[] ByteArray { get; set; }
        public virtual List<TravelPackageAccomodationImage>? TravelPackageAccomodationImages { get; set; }
        public virtual List<TravelPackageLocationImage>? TravelPackageLocationImages { get; set; }
    }
}
