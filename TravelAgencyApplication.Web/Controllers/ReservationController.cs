using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Security.Claims;

namespace TravelAgencyApplication.Web.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SuccessPayment()
        {
            return View();
        }

        //public IActionResult PayOrder(string stripeEmail, string stripeToken)
        //{
        //    StripeConfiguration.ApiKey = "sk_test_51Io84IHBiOcGzrvu4sxX66rTHq8r5nxIxRiJPbOHB4NwVJOE1jSlxgYe741ITs024uXhtpBFtxm3RoCZc3kafocC00IhvgxkL0";
        //    var customerService = new CustomerService();
        //    var chargeService = new ChargeService();
        //    string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    var order = this._shoppingCartService.getShoppingCartInfo(userId);

        //    var customer = customerService.Create(new CustomerCreateOptions
        //    {
        //        Email = stripeEmail,
        //        Source = stripeToken
        //    });

        //    var charge = chargeService.Create(new ChargeCreateOptions
        //    {
        //        Amount = (Convert.ToInt32(order.TotalPrice) * 100),
        //        Description = "Travel Agency Application Payment",
        //        Currency = "usd",
        //        Customer = customer.Id
        //    });

        //    if (charge.Status == "succeeded")
        //    {
        //        this.Order();
        //        return RedirectToAction("SuccessPayment");

        //    }
        //    else
        //    {
        //        return RedirectToAction("NotsuccessPayment");
        //    }
        //}
    }
}
