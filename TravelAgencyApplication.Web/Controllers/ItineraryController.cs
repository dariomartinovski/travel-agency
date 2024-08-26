using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelAgencyApplication.Domain.Model;
using TravelAgencyApplication.Service.Implementation;
using TravelAgencyApplication.Service.Interface;

namespace TravelAgencyApplication.Web.Controllers
{
    [Route("Admin/[controller]")]
    public class ItineraryController : Controller
    {
        private readonly IItineraryService _itineraryService;
        private readonly IDestinationService _destinationService;
        private readonly AuthorizationService _authorizationService;

        public ItineraryController(IItineraryService itineraryService, IDestinationService destinationService, AuthorizationService authorizationService)
        {
            _itineraryService = itineraryService;
            _destinationService = destinationService;
            _authorizationService = authorizationService;
        }

        // GET: Itinerary/Index
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }
            var itineraries = _itineraryService.GetAllItineraries();
            return View(itineraries);
        }

        // GET: Itinerary/Details/5
        [Route("Details/{id?}")]
        public IActionResult Details(Guid id)
        {
            if(id == null)
            {
                return NotFound();
            }
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }
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
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }
            List<Destination> destinations = _destinationService.GetAllDestinations();
            ViewBag.DestinationList = new SelectList(destinations, "Id", null);
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Itinerary itinerary)
        {
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }
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
            if(id == null)
            {
                return NotFound();
            }
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }
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
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
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
            if(id == null)
            {
                return NotFound();
            }
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }
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
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }
            _itineraryService.DeleteItinerary(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
