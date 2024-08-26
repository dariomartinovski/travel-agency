using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelAgencyApplication.Domain.Model;
using TravelAgencyApplication.Service.Implementation;
using TravelAgencyApplication.Service.Interface;

namespace TravelAgencyApplication.Web.Controllers
{
    [Route("Admin/[controller]")]
    public class DestinationController : Controller
    {
        private readonly IDestinationService _destinationService;
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;
        private readonly AuthorizationService _authorizationService;

        public DestinationController(IDestinationService destinationService, ICountryService countryService, ICityService cityService, AuthorizationService authorizationService)
        {
            _destinationService = destinationService;
            _countryService = countryService;
            _cityService = cityService;
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
            var destinations = _destinationService.GetAllDestinations();
            return View(destinations);
        }

        [Route("Create")]
        public IActionResult Create()
        {
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }
            ViewBag.CountryList = new SelectList(_countryService.GetAllCountries(), "Id", "Name");
            ViewBag.CityList = new SelectList(_cityService.GetAllCities(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CountryId,CityId")] Destination destination)
        {
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }
            if (ModelState.IsValid)
            {
                _destinationService.CreateNewDestination(destination);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CountryList = new SelectList(_countryService.GetAllCountries(), "Id", "Name", destination.CountryId);
            ViewBag.CityList = new SelectList(_cityService.GetAllCities(), "Id", "Name", destination.CityId);
            return View(destination);
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
            var destination = _destinationService.GetDetailsForDestination(id);
            if (destination == null)
            {
                return NotFound();
            }
            ViewBag.CountryList = new SelectList(_countryService.GetAllCountries(), "Id", "Name", destination.CountryId);
            ViewBag.CityList = new SelectList(_cityService.GetAllCities(), "Id", "Name", destination.CityId);
            return View(destination);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,CountryId,CityId")] Destination destination)
        {
            if (id != destination.Id)
            {
                return NotFound();
            }
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }
            if (ModelState.IsValid)
            {
                _destinationService.UpdateExistingDestination(destination);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CountryList = new SelectList(_countryService.GetAllCountries(), "Id", "Name", destination.CountryId);
            ViewBag.CityList = new SelectList(_cityService.GetAllCities(), "Id", "Name", destination.CityId);
            return View(destination);
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

            var destination = _destinationService.GetDetailsForDestination(id);
            if (destination == null)
            {
                return NotFound();
            }

            return View(destination);
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
            _destinationService.DeleteDestination(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
