using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelAgencyApplication.Domain.Model;
using TravelAgencyApplication.Service.Interface;

namespace TravelAgencyApplication.Web.Controllers
{
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
        public IActionResult Index()
        {
            var itineraries = _itineraryService.GetAllItineraries();
            return View(itineraries);
        }

        // GET: Itinerary/Details/5
        public IActionResult Details(Guid id)
        {
            var itinerary = _itineraryService.GetDetailsForItinerary(id);
            if (itinerary == null)
            {
                return NotFound();
            }
            return View(itinerary);
        }

        public IActionResult Create()
        {
            List<Destination> destinations = _destinationService.GetAllDestinations();
            ViewBag.DestinationList = new SelectList(destinations, "Id", null);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _itineraryService.DeleteItinerary(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
