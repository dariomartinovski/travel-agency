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
    public class ItineraryService : IItineraryService
    {
        private readonly IRepository<Itinerary> _itineraryRepository;

        public ItineraryService(IRepository<Itinerary> itineraryRepository)
        {
            _itineraryRepository = itineraryRepository;
        }

        public void CreateNewItinerary(Itinerary p)
        {
            _itineraryRepository.Insert(p);
        }

        public void DeleteItinerary(Guid id)
        {
            var itinerary = _itineraryRepository.Get(id);
            if (itinerary != null)
            {
                _itineraryRepository.Delete(itinerary);
            }
        }

        public List<Itinerary> GetAllItinerarys()
        {
            return _itineraryRepository.GetAll().ToList();
        }

        public Itinerary GetDetailsForItinerary(Guid? id)
        {
            return _itineraryRepository.Get(id);
        }

        public void UpdateExistingItinerary(Itinerary p)
        {
            _itineraryRepository.Update(p);
        }
    }
}
