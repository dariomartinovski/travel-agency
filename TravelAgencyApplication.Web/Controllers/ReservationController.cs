using EShop.Domain;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Stripe;
using Stripe.Climate;
using System.Security.Claims;
using System.Text;
using TravelAgencyApplication.Domain.DTO;
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
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");


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
                int currentCapacity = travelPackage.CurrentCapacity - reservation.NumberOfPeople;
                travelPackage.CurrentCapacity = currentCapacity;
                _travelPackageService.UpdateExistingTravelPackage(travelPackage);

                return RedirectToAction("SuccessPayment");
            }
            else
            {
                var reservation = new Reservation
                {
                    UserId = userId,
                    TravelPackageId = travelPackageId,
                    Date = DateTime.Now,
                    NumberOfPeople = numberOfPeople,
                    TotalPrice = totalPrice,
                    ReservationStatus = ReservationStatus.PENDING,
                    HasPaid = false
                };

                _reservationService.CreateNewReservation(reservation);
                int currentCapacity = travelPackage.CurrentCapacity - reservation.NumberOfPeople;
                travelPackage.CurrentCapacity = currentCapacity;
                _travelPackageService.UpdateExistingTravelPackage(travelPackage);
                return RedirectToAction("NotSuccessPayment");
            }
        }
        public FileContentResult CreateReservationInvoice(Guid Id)
        {
            HttpClient client = new HttpClient();
            string URL = "https://localhost:7262/api/Admin/GetDetails";
            var model = new
            {
                Id = Id
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var result = response.Content.ReadAsAsync<ReservationDTO>().Result;

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.docx");
            var document = DocumentModel.Load(templatePath);

            document.Content.Replace("{{ReservationNumber}}", result.Id.ToString());
            document.Content.Replace("{{UserName}}", result.UserName);
            document.Content.Replace("{{TravelPackage}}", result.TravelPackageTitle);

            StringBuilder itineraryList = new StringBuilder();
            foreach (var item in result.ItineraryTitles)
            {
                itineraryList.AppendLine($"{item}");
            }
            document.Content.Replace("{{ItineraryList}}", itineraryList.ToString());

            document.Content.Replace("{{NumberOfPeople}}", result.NumberOfPeople.ToString());
            document.Content.Replace("{{HasPaid}}", result.HasPaid? "Yes" : "No");
            document.Content.Replace("{{TotalPrice}}", result.TotalPrice.ToString() + "$");

            var stream = new MemoryStream();
            document.Save(stream, new PdfSaveOptions());
            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ReservationInvoice.pdf");
        }



    }
}
