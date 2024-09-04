using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Identity;
using TravelAgencyApplication.Domain.Model;

namespace TravelAgencyApplication.Domain.ViewModel
{
    public class TravelPackageViewModel
    {
        public TravelPackage TravelPackage { get; set; }
        public Boolean IsAuthenticated { get; set; }
    }
}
