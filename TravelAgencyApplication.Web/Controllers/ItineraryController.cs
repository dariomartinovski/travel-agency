using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelAgencyApplication.Domain.Model;
using TravelAgencyApplication.Service.Interface;

namespace TravelAgencyApplication.Web.Controllers
{
    [Route("Admin/[controller]")]
    public class ItineraryController : Controller
    {
        private readonly IItineraryService _itineraryService;
        private readonly IDestinationService _destinationService;

        public ItineraryController(IItineraryService itineraryService, IDestinationService destinationService)
        {
            _itineraryService = itineraryService;
            _destinationService = destinationService;
        }

        // GET: Itinerary/Index
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            var itineraries = _itineraryService.GetAllItineraries();
            return View(itineraries);
        }

        // GET: Itinerary/Details/5
        [Route("Details/{id?}")]
        public IActionResult Details(Guid id)
        {
            var itinerary = _itineraryService.GetDetailsForItinerary(id);
            if (itinerary == null)
            {
                return NotFound();
            }
            return View(itinerary);
        }
        [Route("Create")]
        public IActionResult Create()
        {
            List<Destination> destinations = _destinationService.GetAllDestinations();
            ViewBag.DestinationList = new SelectList(destinations, "Id", null);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public IActionResult Create(Itinerary itinerary)
        {
            if (ModelState.IsValid)
            {
                _itineraryService.CreateNewItinerary(itinerary);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Destinations = _destinationService.GetAllDestinations();
            return View(itinerary);
        }
        [Route("Edit/{id?}")]
        public IActionResult Edit(Guid id)
        {
            var itinerary = _itineraryService.GetDetailsForItinerary(id);
            if (itinerary == null)
            {
                return NotFound();
            }
            List<Destination> destinations = _destinationService.GetAllDestinations();
            ViewBag.DestinationList = new SelectList(destinations, "Id", null);
            return View(itinerary);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, Itinerary itinerary)
        {
            if (id != itinerary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _itineraryService.UpdateExistingItinerary(itinerary);
                return RedirectToAction(nameof(Index));
            }
            List<Destination> destinations = _destinationService.GetAllDestinations();
            ViewBag.DestinationList = new SelectList(destinations, "Id", null);
            return View(itinerary);
        }
        [Route("Delete/{id?}")]
        public IActionResult Delete(Guid id)
        {
            var itinerary = _itineraryService.GetDetailsForItinerary(id);
            if (itinerary == null)
            {
                return NotFound();
            }
            return View(itinerary);
        }

        [HttpPost, ActionName("Delete")]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _itineraryService.DeleteItinerary(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
