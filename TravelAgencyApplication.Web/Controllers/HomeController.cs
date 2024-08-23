using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TravelAgencyApplication.Domain.Enum;
using TravelAgencyApplication.Service.Interface;
using TravelAgencyApplication.Web.Models;

namespace TravelAgencyApplication.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;


        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return Redirect("/Identity/Account/Login");
            }
            var currentUser = _userService.GetDetailsForTAUser(userId);
           
            return View(currentUser);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
