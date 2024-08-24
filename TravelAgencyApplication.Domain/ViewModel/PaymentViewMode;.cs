using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyApplication.Domain.ViewModel
{
    public class PaymentViewModel
    {
        public Guid TravelPackageId { get; set; }
        public int NumberOfPeople { get; set; }
        public string StripeEmail { get; set; }
        public string StripeToken { get; set; }

        public double TotalPrice { get; set; }
    }
}
