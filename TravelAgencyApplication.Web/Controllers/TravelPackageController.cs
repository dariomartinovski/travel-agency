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
using TravelAgencyApplication.Web.Data;

namespace TravelAgencyApplication.Web.Controllers
{
    public class TravelPackageController : Controller
    {
        private readonly ITravelPackageService _travelPackageService;
        private readonly ITravelPackageItineraryService _travelPackageItineraryService;
        private readonly ITravelPackageTagService _travelPackageTagService;
        private readonly ITravelPackageDepartureLocationService _travelPackageDepartureLocationService;
        private readonly IUserService _userService;
        private readonly IDestinationService _destinationService;
        private readonly IItineraryService _itineraryService;
        private readonly IDepartureLocationService _departureLocationService;
        private readonly ITagService _tagService;
        private readonly AuthorizationService _authorizationService;

        public TravelPackageController(ITravelPackageTagService travelPackageTagService, ITravelPackageDepartureLocationService travelPackageDepartureLocationService, ITravelPackageItineraryService travelPackageItineraryService, ITravelPackageService travelPackageService, IUserService userService, IDepartureLocationService departureLocationService, IItineraryService itineraryService, IDestinationService destinationService, ITagService tagService, AuthorizationService authorizationService)
        {
            _travelPackageService = travelPackageService;
            _userService = userService;
            _departureLocationService = departureLocationService;
            _itineraryService = itineraryService;
            _destinationService = destinationService;
            _tagService = tagService;
            _travelPackageItineraryService = travelPackageItineraryService;
            _travelPackageDepartureLocationService = travelPackageDepartureLocationService;
            _travelPackageTagService = travelPackageTagService;
            _authorizationService = authorizationService;
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

        [Route("Admin/[controller]")]
        [Route("Admin/[controller]/Index")]
        public IActionResult AdminIndex()
        {
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }
            var travelPackages = _travelPackageService.GetAllTravelPackages();
            return View(travelPackages);

        }

        [Route("Admin/[controller]/Details/{id?}")]
        public async Task<IActionResult> AdminDetails(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }

            var travelPackage = _travelPackageService.GetDetailsForTravelPackage(id);
            if (travelPackage == null)
            {
                return NotFound();
            }
            return View(travelPackage);
        }

        [Route("Admin/[controller]/Create")]
        public IActionResult Create()
        {
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }
            ViewBag.DestinationList = new SelectList(_destinationService.GetAllDestinations(), "Id", null);
            ViewBag.UserList = new SelectList(_userService.GetAllGuides(), "Id", "FirstName");
            ViewBag.TagList = new SelectList(_tagService.GetAllTags(), "Id", "Name");
            ViewBag.ItineraryList = new SelectList(_itineraryService.GetAllItineraries(), "Id", "Title");
            ViewBag.DepartureLocationList = new SelectList(_departureLocationService.GetAllDepartureLocations(), "Id", null);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/[controller]/Create")]
        public IActionResult Create([Bind("Title, Details, BasePrice, DepartureDate, ReturnDate, DestinationId, MaxCapacity, Season, UserId, TransportType, Tags, Itineraries, DepartureLocations")] TravelPackageDTO travelPackageDto)
        {
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }
            if (ModelState.IsValid)
            {
                List<Itinerary> Itineraries = _itineraryService.GetAllItinerariesByIds(travelPackageDto.Itineraries);
                List<DepartureLocation> departureLocations= _departureLocationService.GetAllDepartureLocationsByIds(travelPackageDto.DepartureLocations);
                List<Tag> tags = _tagService.GetAllTagsByIds(travelPackageDto.Tags);

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
                    Tags = new List<TravelPackageTag>(),
                    Itineraries = new List<TravelPackageItinerary>(),
                    DepartureLocations = new List<TravelPackageDepartureLocation>()
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

                var travelPackageTags = new List<TravelPackageTag>();
                tags.ForEach(t => travelPackageTags.Add(new TravelPackageTag
                {
                    Id = Guid.NewGuid(),
                    TravelPackage = travelPackage,
                    TravelPackageId = travelPackage.Id,
                    Tag = t,
                    TagId = t.Id
                }));

                var travelPackageDepartureLocations = new List<TravelPackageDepartureLocation>();
                departureLocations.ForEach(dl => travelPackageDepartureLocations.Add(new TravelPackageDepartureLocation
                {
                    Id = Guid.NewGuid(),
                    TravelPackage = travelPackage,
                    TravelPackageId = travelPackage.Id,
                    DepartureLocation = dl,
                    DepartureLocationId = dl.Id
                }));

                travelPackage.Itineraries = travelPackageItineraries;
                travelPackage.Tags = travelPackageTags;
                travelPackage.DepartureLocations = travelPackageDepartureLocations;

                _travelPackageService.CreateNewTravelPackage(travelPackage);
                return RedirectToAction(nameof(AdminIndex));
            }
            return View(travelPackageDto);
        }

        // GET: TravelPackage/Edit/5
        [Route("Admin/[controller]/Edit/{id?}")]
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
            var travelPackage = _travelPackageService.GetDetailsForTravelPackage(id);
            if (travelPackage == null)
            {
                return NotFound();
            }

            ViewBag.DestinationList = new SelectList(_destinationService.GetAllDestinations(), "Id", null, travelPackage.DestinationId);
            ViewBag.UserList = new SelectList(_userService.GetAllTAUsers(), "Id", "FirstName", travelPackage.UserId);
            ViewBag.TagList = new MultiSelectList(_tagService.GetAllTags(), "Id", "Name", travelPackage.Tags.Select(t => t.TagId));
            ViewBag.ItineraryList = new MultiSelectList(_itineraryService.GetAllItineraries(), "Id", "Title", travelPackage.Itineraries.Select(i => i.ItineraryId));
            ViewBag.DepartureLocationList = new MultiSelectList(_departureLocationService.GetAllDepartureLocations(), "Id", null, travelPackage.DepartureLocations.Select(dl => dl.DepartureLocationId));

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
                Tags = travelPackage.Tags.Select(t => t.TagId).ToList(),
                Itineraries = travelPackage.Itineraries.Select(i => i.ItineraryId).ToList(),
                DepartureLocations = travelPackage.DepartureLocations.Select(dl => dl.DepartureLocationId).ToList()
            };

            return View(travelPackageDto);
        }


        // POST: TravelPackage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/[controller]/Edit/{id}")]
        public IActionResult Edit(Guid id, [Bind("Id, Title, Details, BasePrice, DepartureDate, ReturnDate, DestinationId, MaxCapacity, Season, UserId, TransportType, Tags, Itineraries, DepartureLocations")] TravelPackageDTO travelPackageDto)
        {
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }

            if (id != travelPackageDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    TravelPackage package = _travelPackageService.GetDetailsForTravelPackage(id);
                    if (package != null)
                    {

                        package.Tags.Clear();
                        package.Itineraries.Clear();
                        package.DepartureLocations.Clear();
                        _travelPackageService.UpdateExistingTravelPackage(package);

                        List<Itinerary> Itineraries = _itineraryService.GetAllItinerariesByIds(travelPackageDto.Itineraries);
                        List<DepartureLocation> departureLocations = _departureLocationService.GetAllDepartureLocationsByIds(travelPackageDto.DepartureLocations);
                        List<Tag> tags = _tagService.GetAllTagsByIds(travelPackageDto.Tags);

                        List<TravelPackageItinerary> travelPackageItineraries = Itineraries.Select(x => new TravelPackageItinerary
                        {
                            Id = Guid.NewGuid(),
                            TravelPackage = package,
                            TravelPackageId = package.Id,
                            Itinerary = x,
                            ItineraryId = x.Id
                        }).ToList();

                        var travelPackageTags = new List<TravelPackageTag>();
                        tags.ForEach(t => travelPackageTags.Add(new TravelPackageTag
                        {
                            Id = Guid.NewGuid(),
                            TravelPackage = package,
                            TravelPackageId = package.Id,
                            Tag = t,
                            TagId = t.Id
                        }));

                        var travelPackageDepartureLocations = new List<TravelPackageDepartureLocation>();
                        departureLocations.ForEach(dl => travelPackageDepartureLocations.Add(new TravelPackageDepartureLocation
                        {
                            Id = Guid.NewGuid(),
                            TravelPackage = package,
                            TravelPackageId = package.Id,
                            DepartureLocation = dl,
                            DepartureLocationId = dl.Id
                        }));

                        travelPackageItineraries.ForEach(_travelPackageItineraryService.CreateNewTravelPackageItinerary);
                        travelPackageTags.ForEach(_travelPackageTagService.CreateNewTravelPackageTag);
                        travelPackageDepartureLocations.ForEach(_travelPackageDepartureLocationService.CreateNewTravelPackageDepartureLocation);

                        package.Itineraries = travelPackageItineraries;
                        package.Tags = travelPackageTags;
                        package.DepartureLocations = travelPackageDepartureLocations;

                        package.Title = travelPackageDto.Title;
                        package.Details = travelPackageDto.Details;
                        package.BasePrice = travelPackageDto.BasePrice;
                        package.DepartureDate = travelPackageDto.DepartureDate;
                        package.ReturnDate = travelPackageDto.ReturnDate;
                        package.MaxCapacity = travelPackageDto.MaxCapacity;
                        package.CurrentCapacity = travelPackageDto.MaxCapacity;
                        package.DestinationId = travelPackageDto.DestinationId;
                        package.Destination = _destinationService.GetDetailsForDestination(travelPackageDto.DestinationId);
                        package.Season = (Season)travelPackageDto.Season;
                        package.TransportType = (TransportType)travelPackageDto.TransportType;
                        package.UserId = travelPackageDto.UserId;
                        package.Guide = _userService.GetDetailsForTAUser(travelPackageDto.UserId);

                        _travelPackageService.UpdateExistingTravelPackage(package);
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(AdminIndex));
            }
            return View(travelPackageDto);
        }

        [Route("Admin/[controller]/Delete/{id?}")]
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
            var travelPackage = _travelPackageService.GetDetailsForTravelPackage(id);
            if (travelPackage == null)
            {
                return NotFound();
            }
            return View(travelPackage);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/[controller]/Delete/{id?}")]
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

                    return RedirectToAction(nameof(AdminIndex));;
                }
            }
            return RedirectToAction(nameof(AdminIndex)); ;
        }

    }
}
