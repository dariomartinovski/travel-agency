using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyApplication.Domain.Model
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<TravelPackageTag>? TravelPackages { get; set; }
    }
}
