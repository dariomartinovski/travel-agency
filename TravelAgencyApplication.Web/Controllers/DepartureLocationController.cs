using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelAgencyApplication.Domain.Model;
using TravelAgencyApplication.Service.Implementation;
using TravelAgencyApplication.Service.Interface;

namespace TravelAgencyApplication.Web.Controllers
{
    [Route("Admin/[controller]")]
    public class DepartureLocationController : Controller
    {
        private readonly IDepartureLocationService _departureLocationService;
        private readonly ITravelPackageDepartureLocationService _travelPackageDepartureLocationService;
        private readonly ICityService _cityService;
        private readonly AuthorizationService _authorizationService;

        public DepartureLocationController(IDepartureLocationService departureLocationService, ICityService cityService, ITravelPackageDepartureLocationService travelPackageDepartureLocationService, AuthorizationService authorizationService)
        {
            _departureLocationService = departureLocationService;
            _cityService = cityService;
            _travelPackageDepartureLocationService = travelPackageDepartureLocationService;
            _authorizationService = authorizationService;
        }

        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }
            var departureLocations = _departureLocationService.GetAllDepartureLocations();
            return View(departureLocations);
        }

        [Route("Create")]
        public IActionResult Create()
        {
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }
            ViewBag.CityList = new SelectList(_cityService.GetAllCities(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CityId,Location,DepartureTime")] DepartureLocation departureLocation)
        {
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }
            if (ModelState.IsValid)
            {
                _departureLocationService.CreateNewDepartureLocation(departureLocation);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CityList = new SelectList(_cityService.GetAllCities(), "Id", "Name", departureLocation.CityId);
            return View(departureLocation);
        }

        [Route("Edit/{id?}")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }

            var departureLocation = _departureLocationService.GetDetailsForDepartureLocation(id);
            if (departureLocation == null)
            {
                return NotFound();
            }
            ViewBag.CityList = new SelectList(_cityService.GetAllCities(), "Id", "Name", departureLocation.CityId);
            return View(departureLocation);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,CityId,Location,DepartureTime")] DepartureLocation departureLocation)
        {
            if (id != departureLocation.Id)
            {
                return NotFound();
            }
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }
            if (ModelState.IsValid)
            {
                _departureLocationService.UpdateExistingDepartureLocation(departureLocation);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CityList = new SelectList(_cityService.GetAllCities(), "Id", "Name", departureLocation.CityId);
            return View(departureLocation);
        }

        [Route("Delete/{id?}")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }

            var departureLocation = _departureLocationService.GetDetailsForDepartureLocation(id);
            if (departureLocation == null)
            {
                return NotFound();
            }

            return View(departureLocation);
        }

        [HttpPost, ActionName("Delete")]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }
            _departureLocationService.DeleteDepartureLocation(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
