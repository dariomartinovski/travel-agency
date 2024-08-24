using EShop.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using System.Security.Claims;
using TravelAgencyApplication.Domain.Enum;
using TravelAgencyApplication.Domain.Model;
using TravelAgencyApplication.Domain.ViewModel;
using TravelAgencyApplication.Service.Interface;

namespace TravelAgencyApplication.Web.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ITravelPackageService _travelPackageService;
        private readonly IReservationService _reservationService;
        private readonly StripeSettings _stripeSettings;

        public ReservationController(ITravelPackageService travelPackageService, IReservationService reservationService, IOptions<StripeSettings> stripeSettings)
        {
            _travelPackageService = travelPackageService;
            _reservationService = reservationService;
            _stripeSettings = stripeSettings.Value;

        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SuccessPayment()
        {
            return View();
        }

        public IActionResult NotSuccessPayment()
        {
            return View();
        }
       public IActionResult PayOrder(Guid travelPackageId, int numberOfPeople)
        {
            var travelPackage = _travelPackageService.GetDetailsForTravelPackage(travelPackageId);
            int totalPrice = (int)(travelPackage.BasePrice * numberOfPeople);
            var model = new PaymentViewModel
            {
                TravelPackageId = travelPackageId,
                NumberOfPeople = numberOfPeople,
                TotalPrice = totalPrice
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult PayOrder(Guid travelPackageId, int numberOfPeople, string stripeEmail, string stripeToken)
        {
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
            var customerService = new CustomerService();
            var chargeService = new ChargeService();
           
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Redirect("/Identity/Account/Login");
            }

            var travelPackage = _travelPackageService.GetDetailsForTravelPackage(travelPackageId);

            if (travelPackage == null)
            {
                return RedirectToAction("NotSuccessPayment");
            }


            int totalPrice = (int)(travelPackage.BasePrice * numberOfPeople);


            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });


            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = totalPrice * 100, 
                Description = "Travel Agency Application Payment",
                Currency = "usd",
                Customer = customer.Id
            });



            if (charge.Status == "succeeded")
            {

                var reservation = new Reservation
                {
                    UserId = userId,
                    TravelPackageId = travelPackageId,
                    Date = DateTime.Now,
                    NumberOfPeople = numberOfPeople,
                    TotalPrice = totalPrice,
                    ReservationStatus = ReservationStatus.CONFIRMED,
                    HasPaid = true
                };

                _reservationService.CreateNewReservation(reservation);

                return RedirectToAction("SuccessPayment");
            }
            else
            {
                return RedirectToAction("NotSuccessPayment");
            }
        }

    }
}
