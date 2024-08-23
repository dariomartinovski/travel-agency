using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Model;
using TravelAgencyApplication.Repository.Interface;
using TravelAgencyApplication.Service.Interface;

namespace TravelAgencyApplication.Service.Implementation
{
    public class TravelPackageItineraryService : ITravelPackageItineraryService
    {
        private readonly IRepository<TravelPackageItinerary> _travelPackageRepository;

        public TravelPackageItineraryService(IRepository<TravelPackageItinerary> travelPackageRepository)
        {
            _travelPackageRepository = travelPackageRepository;
        }

        public void CreateNewTravelPackageItinerary(TravelPackageItinerary p)
        {
            _travelPackageRepository.Insert(p);
        }

        public void DeleteTravelPackageItinerary(Guid? id)
        {
            TravelPackageItinerary tp = _travelPackageRepository.Get(id);
            if (tp != null)
            {
                _travelPackageRepository.Delete(tp);
            }
        }

        public List<TravelPackageItinerary> GetAllTravelPackageItinerarys()
        {
            return _travelPackageRepository.GetAll().ToList();
        }

        public List<TravelPackageItinerary> GetAllTravelPackageItinerariesByIds(List<Guid> ids)
        {
            return _travelPackageRepository.GetAll().Where(i => ids.Contains(i.Id)).ToList();
        }


        public TravelPackageItinerary GetDetailsForTravelPackageItinerary(Guid? id)
        {
            return _travelPackageRepository.Get(id);
        }

        public void UpdateExistingTravelPackageItinerary(TravelPackageItinerary p)
        {
            _travelPackageRepository.Update(p);
        }

        //public List<TravelPackageItinerary> UpdateTravelPackageItineraries(List<TravelPackageItinerary> existingItineraries, List<TravelPackageItinerary> newItineraries)
        //{
        //    // 1 2 3   -  1 4  - Maybe another day
        //    //List<TravelPackageItinerary> itinerariesToAdd = newItineraries.Where(i => !existingItineraries.Select(it => it.ItineraryId).Contains(i.ItineraryId)).ToList();
        //    //List<TravelPackageItinerary> itinerariesToDelete = existingItineraries.Where(e => !newItineraries.Select(i => i.ItineraryId).Contains(e.ItineraryId)).ToList();

        //    //itinerariesToDelete.ForEach(i =>_travelPackageRepository.Delete(i));
        //    //itinerariesToAdd.ForEach(i => _travelPackageRepository.Insert(i));

        //    existingItineraries.ForEach(i =>  _travelPackageRepository.Delete(i));
        //    newItineraries.ForEach(i =>  _travelPackageRepository.Insert(i));
        //    return newItineraries;
        //}
        public List<TravelPackageItinerary> UpdateTravelPackageItineraries(List<TravelPackageItinerary> existingItineraries, List<TravelPackageItinerary> newItineraries)
        {
            
            var itinerariesToDelete = existingItineraries
                .Where(e => !newItineraries.Select(i => i.ItineraryId).Contains(e.ItineraryId))
                .ToList();

            // Find itineraries to add (new ones not in the existing list)
            var itinerariesToAdd = newItineraries
                .Where(n => !existingItineraries.Select(e => e.ItineraryId).Contains(n.ItineraryId))
                .ToList();

            foreach (var itinerary in itinerariesToDelete)
            {
                DeleteTravelPackageItinerary(itinerary.Id);
                //_travelPackageRepository.Delete(itinerary);
            }

            // Add new itineraries
            foreach (var itinerary in itinerariesToAdd)
            {
                CreateNewTravelPackageItinerary(itinerary);
                //_travelPackageRepository.Insert(itinerary);
            }

            // Return the updated list of itineraries
            return existingItineraries
                .Where(e => !itinerariesToDelete.Contains(e)) // Keep existing itineraries that are not deleted
                .Concat(itinerariesToAdd) // Add new itineraries
                .ToList();
        }


    }
}
