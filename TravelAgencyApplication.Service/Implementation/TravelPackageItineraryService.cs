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

        public TravelPackageItinerary GetDetailsForTravelPackageItinerary(Guid? id)
        {
            return _travelPackageRepository.Get(id);
        }

        public void UpdateExistingTravelPackageItinerary(TravelPackageItinerary p)
        {
            _travelPackageRepository.Update(p);
        }
    }
}
