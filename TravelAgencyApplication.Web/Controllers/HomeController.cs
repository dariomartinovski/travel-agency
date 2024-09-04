using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TravelAgencyApplication.Domain.Enum;
using TravelAgencyApplication.Service.Interface;
using TravelAgencyApplication.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelAgencyApplication.Service.Implementation;
using TravelAgencyApplication.Domain.Model;

namespace TravelAgencyApplication.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly ITravelPackageService _travelPackageService;

        public HomeController(ITravelPackageService travelPackageService, ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
            _travelPackageService = travelPackageService;

        }

        public IActionResult Index()
        {
            List<TravelPackage> TravelPackages = _travelPackageService.GetAllTravelPackages().Take(6).ToList();
            return View(TravelPackages);
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
