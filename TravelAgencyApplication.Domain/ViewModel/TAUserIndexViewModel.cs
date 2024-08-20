using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Enum;
using TravelAgencyApplication.Domain.Identity;

namespace TravelAgencyApplication.Domain.ViewModel
{
    public class TAUserIndexViewModel
    {
       public List<TAUser> Users {  get; set; }
       public UserRole CurrentUserRole { get; set; }
    }
}
