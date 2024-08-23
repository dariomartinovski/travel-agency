﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelAgencyApplication.Domain.Model;
using TravelAgencyApplication.Service.Interface;

namespace TravelAgencyApplication.Web.Controllers
{
    public class DestinationController : Controller
    {
        private readonly IDestinationService _destinationService;
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;

        public DestinationController(IDestinationService destinationService, ICountryService countryService, ICityService cityService)
        {
            _destinationService = destinationService;
            _countryService = countryService;
            _cityService = cityService;
        }

        public IActionResult Index()
        {
            var destinations = _destinationService.GetAllDestinations();
            return View(destinations);
        }

        public IActionResult Create()
        {
            ViewBag.CountryList = new SelectList(_countryService.GetAllCountries(), "Id", "Name");
            ViewBag.CityList = new SelectList(_cityService.GetAllCities(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CountryId,CityId")] Destination destination)
        {
            if (ModelState.IsValid)
            {
                _destinationService.CreateNewDestination(destination);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CountryList = new SelectList(_countryService.GetAllCountries(), "Id", "Name", destination.CountryId);
            ViewBag.CityList = new SelectList(_cityService.GetAllCities(), "Id", "Name", destination.CityId);
            return View(destination);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
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
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,CountryId,CityId")] Destination destination)
        {
            if (id != destination.Id)
            {
                return NotFound();
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

        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destination = _destinationService.GetDetailsForDestination(id);
            if (destination == null)
            {
                return NotFound();
            }

            return View(destination);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _destinationService.DeleteDestination(id);
            return RedirectToAction(nameof(Index));
        }
    }
}