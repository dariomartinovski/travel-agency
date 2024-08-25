using Microsoft.AspNetCore.Mvc;
using TravelAgencyApplication.Domain.Model;
using TravelAgencyApplication.Service.Interface;

namespace TravelAgencyApplication.Web.Controllers
{
    [Route("Admin/[controller]")]
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            var countries = _countryService.GetAllCountries();
            return View(countries);
        }

        [Route("Details/{id?}")]
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = _countryService.GetDetailsForCountry(id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name")] Country country)
        {
            if (ModelState.IsValid)
            {
                _countryService.CreateNewCountry(country);
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        [Route("Edit/{id?}")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = _countryService.GetDetailsForCountry(id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Name")] Country country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _countryService.UpdateExistingCountry(country);
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        [Route("Delete/{id?}")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = _countryService.GetDetailsForCountry(id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        [HttpPost, ActionName("Delete")]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _countryService.DeleteCountry(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
