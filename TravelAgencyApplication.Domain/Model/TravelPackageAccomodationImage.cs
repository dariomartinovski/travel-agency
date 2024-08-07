using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyApplication.Domain.Model
{
    public class TravelPackageAccomodationImage: BaseEntity
    {
        public TravelPackage? TravelPackage { get; set; }

        public Guid TravelPackageId { get; set; }

        public Image? Image { get; set; }

        public Guid ImageId { get; set; }
    }
}
