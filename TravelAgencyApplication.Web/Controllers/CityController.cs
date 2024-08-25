using Microsoft.AspNetCore.Mvc;
using TravelAgencyApplication.Domain.Model;
using TravelAgencyApplication.Service.Interface;

namespace TravelAgencyApplication.Web.Controllers
{
    [Route("Admin/[controller]")]
    public class CityController : Controller
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            var cities = _cityService.GetAllCities();
            return View(cities);
        }

        [Route("Details/{id?}")]
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = _cityService.GetDetailsForCity(id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name")] City city)
        {
            if (ModelState.IsValid)
            {
                city.Id = Guid.NewGuid();
                _cityService.CreateNewCity(city);
                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }

        [Route("Edit/{id?}")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = _cityService.GetDetailsForCity(id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id, Name")] City city)
        {
            if (id != city.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _cityService.UpdateExistingCity(city);
                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }

        [Route("Delete/{id?}")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = _cityService.GetDetailsForCity(id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        [HttpPost, ActionName("Delete")]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _cityService.DeleteCity(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
