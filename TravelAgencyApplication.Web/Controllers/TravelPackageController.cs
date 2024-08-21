using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using TravelAgencyApplication.Domain.DTO;
using TravelAgencyApplication.Domain.Enum;
using TravelAgencyApplication.Domain.Identity;
using TravelAgencyApplication.Domain.Model;
using TravelAgencyApplication.Service.Implementation;
using TravelAgencyApplication.Service.Interface;

namespace TravelAgencyApplication.Web.Controllers
{
    public class TravelPackageController : Controller
    {
        private readonly ITravelPackageService _travelPackageService;
        private readonly ITravelPackageItineraryService _travelPackageItineraryService;
        private readonly IUserService _userService;
        private readonly IDestinationService _destinationService;
        private readonly IItineraryService _itineraryService;
        private readonly IDepartureLocationService _departureLocationService;
        private readonly ITagService _tagService;

        public TravelPackageController(ITravelPackageItineraryService travelPackageItineraryService, ITravelPackageService travelPackageService, IUserService userService, IDepartureLocationService departureLocationService, IItineraryService itineraryService, IDestinationService destinationService, ITagService tagService)
        {
            _travelPackageService = travelPackageService;
            _userService = userService;
            _departureLocationService = departureLocationService;
            _itineraryService = itineraryService;
            _destinationService = destinationService;
            _tagService = tagService;
            _travelPackageItineraryService = travelPackageItineraryService;
        }

        public IActionResult Index()
        {
            var travelPackages = _travelPackageService.GetAllTravelPackages();
            return View(travelPackages);
           
        }
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelPackage = _travelPackageService.GetDetailsForTravelPackage(id);
            if (travelPackage == null)
            {
                return NotFound();
            }
            return View(travelPackage);
        }

        public IActionResult Create()
        {
            ViewBag.DestinationList = new SelectList(_destinationService.GetAllDestinations(), "Id", null);
            ViewBag.UserList = new SelectList(_userService.GetAllTAUsers(), "Id", "FirstName");
            ViewBag.TagList = new SelectList(_tagService.GetAllTags(), "Id", "Name");
            ViewBag.ItineraryList = new SelectList(_itineraryService.GetAllItineraries(), "Id", "Title");
            ViewBag.DepartureLocationList = new SelectList(_departureLocationService.GetAllDepartureLocations(), "Id", null);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title, Details, BasePrice, DepartureDate, ReturnDate, DestinationId, MaxCapacity, Season, UserId, TransportType, Tags, Itineraries, DepartureLocations")] TravelPackageDTO travelPackageDto)
        {
            if (ModelState.IsValid)
            {
                List<Itinerary> Itineraries = _itineraryService.GetAllItinerariesByIds(travelPackageDto.Itineraries);

                TravelPackage travelPackage = new TravelPackage
                {
                    Id = Guid.NewGuid(),
                    Title = travelPackageDto.Title,
                    Details = travelPackageDto.Details,
                    BasePrice = travelPackageDto.BasePrice,
                    DepartureDate = travelPackageDto.DepartureDate,
                    ReturnDate = travelPackageDto.ReturnDate,
                    MaxCapacity = travelPackageDto.MaxCapacity,
                    CurrentCapacity = travelPackageDto.MaxCapacity,
                    DestinationId = travelPackageDto.DestinationId,
                    Destination = _destinationService.GetDetailsForDestination(travelPackageDto.DestinationId),
                    Season = travelPackageDto.Season,
                    TransportType = travelPackageDto.TransportType,
                    UserId = travelPackageDto.UserId,
                    Guide = _userService.GetDetailsForTAUser(travelPackageDto.UserId),
                    Tags = _tagService.GetAllTagsByIds(travelPackageDto.Tags),
                    Itineraries = new List<TravelPackageItinerary>(),
                    DepartureLocations = _departureLocationService.GetAllDepartureLocationsByIds(travelPackageDto.DepartureLocations)
                };
                var travelPackageItineraries = new List<TravelPackageItinerary>();
                Itineraries.ForEach(x => travelPackageItineraries.Add(new TravelPackageItinerary
                {
                    Id = Guid.NewGuid(),
                    TravelPackage = travelPackage,
                    TravelPackageId = travelPackage.Id,
                    Itinerary = x,
                    ItineraryId = x.Id
                }));


                travelPackage.Itineraries = travelPackageItineraries;

                _travelPackageService.CreateNewTravelPackage(travelPackage);
                return RedirectToAction(nameof(Index));
            }
            return View(travelPackageDto);
        }

        // GET: TravelPackage/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelPackage = _travelPackageService.GetDetailsForTravelPackage(id);
            if (travelPackage == null)
            {
                return NotFound();
            }

            ViewBag.DestinationList = new SelectList(_destinationService.GetAllDestinations(), "Id", null, travelPackage.DestinationId);
            ViewBag.UserList = new SelectList(_userService.GetAllTAUsers(), "Id", "FirstName", travelPackage.UserId);
            ViewBag.TagList = new MultiSelectList(_tagService.GetAllTags(), "Id", "Name", travelPackage.Tags.Select(t => t.Id));
            ViewBag.ItineraryList = new MultiSelectList(_itineraryService.GetAllItineraries(), "Id", "Title", travelPackage.Itineraries.Select(i => i.ItineraryId));
            ViewBag.DepartureLocationList = new MultiSelectList(_departureLocationService.GetAllDepartureLocations(), "Id", null, travelPackage.DepartureLocations.Select(dl => dl.Id));

            var travelPackageDto = new TravelPackageDTO
            {
                Id = travelPackage.Id,
                Title = travelPackage.Title,
                Details = travelPackage.Details,
                BasePrice = travelPackage.BasePrice,
                DepartureDate = travelPackage.DepartureDate,
                ReturnDate = travelPackage.ReturnDate,
                DestinationId = travelPackage.DestinationId,
                MaxCapacity = travelPackage.MaxCapacity,
                Season = travelPackage.Season,
                UserId = travelPackage.UserId,
                TransportType = travelPackage.TransportType,
                Tags = travelPackage.Tags.Select(t => t.Id).ToList(),
                Itineraries = travelPackage.Itineraries.Select(i => i.ItineraryId).ToList(),
                DepartureLocations = travelPackage.DepartureLocations.Select(dl => dl.Id).ToList()
            };

            return View(travelPackageDto);
        }


        // POST: TravelPackage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id, Title, Details, BasePrice, DepartureDate, ReturnDate, DestinationId, MaxCapacity, Season, UserId, TransportType, Tags, Itineraries, DepartureLocations")] TravelPackageDTO travelPackageDto)
        {
            if (id != travelPackageDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    List<Itinerary> Itineraries = _itineraryService.GetAllItinerariesByIds(travelPackageDto.Itineraries);

                    var travelPackage = new TravelPackage
                    {
                        Id = travelPackageDto.Id,
                        Title = travelPackageDto.Title,
                        Details = travelPackageDto.Details,
                        BasePrice = travelPackageDto.BasePrice,
                        DepartureDate = travelPackageDto.DepartureDate,
                        ReturnDate = travelPackageDto.ReturnDate,
                        DestinationId = travelPackageDto.DestinationId,
                        MaxCapacity = travelPackageDto.MaxCapacity,
                        Season = travelPackageDto.Season,
                        UserId = travelPackageDto.UserId,
                        TransportType = travelPackageDto.TransportType,
                        Tags = _tagService.GetAllTagsByIds(travelPackageDto.Tags),
                        Itineraries = _travelPackageItineraryService.GetAllTravelPackageItinerariesByIds(travelPackageDto.Itineraries),
                        DepartureLocations = _departureLocationService.GetAllDepartureLocationsByIds(travelPackageDto.DepartureLocations)
                    };

                    _travelPackageService.UpdateExistingTravelPackage(travelPackage);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(travelPackageDto);
        }

        // GET: TravelPackage/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelPackage = _travelPackageService.GetDetailsForTravelPackage(id);
            if (travelPackage == null)
            {
                return NotFound();
            }

            return View(travelPackage);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null && id != null)
            {
                var user = _userService.GetDetailsForTAUser(userId);
                if (user.UserRole == Domain.Enum.UserRole.ADMIN)
                {
                    var travelPackage = _travelPackageService.GetDetailsForTravelPackage(id);
                    if (travelPackage != null)
                    {
                        _travelPackageService.DeleteTravelPackage(id);
                    }

                    return RedirectToAction(nameof(Index));;
                }
            }
            return RedirectToAction(nameof(Index)); ;

            //return Unauthorized();
        }

    }
}
