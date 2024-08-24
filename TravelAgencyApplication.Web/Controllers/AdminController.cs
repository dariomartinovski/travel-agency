using Microsoft.AspNetCore.Mvc;
using TravelAgencyApplication.Domain.DTO;
using TravelAgencyApplication.Service.Interface;

namespace TravelAgencyApplication.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;
        private readonly IDepartureLocationService _departureLocationService;
        private readonly IDestinationService _destinationService;
        private readonly IItineraryService _itineraryService;
        private readonly ITagService _tagService;
        private readonly ITravelPackageService _travelPackageService;
        private readonly IUserService _userService;

        public AdminController(ICityService cityService, ICountryService countryService, IDepartureLocationService departureLocationService, IDestinationService destinationService, IItineraryService itineraryService, ITagService tagService, ITravelPackageService travelPackageService, IUserService userService)
        {
            _cityService = cityService;
            _countryService = countryService;
            _departureLocationService = departureLocationService;
            _destinationService = destinationService;
            _itineraryService = itineraryService;
            _tagService = tagService;
            _travelPackageService = travelPackageService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var model = new AdminPageDTO
            {
                Cities = _cityService.GetAllCities(),
                Countries = _countryService.GetAllCountries(),
                DepartureLocations = _departureLocationService.GetAllDepartureLocations(),
                Destinations = _destinationService.GetAllDestinations(),
                Itineraries = _itineraryService.GetAllItineraries(),
                Tags = _tagService.GetAllTags(),
                TravelPackages = _travelPackageService.GetAllTravelPackages(),
                Users = _userService.GetAllTAUsers(),
            };
            return View(model);
        }
    }
}
