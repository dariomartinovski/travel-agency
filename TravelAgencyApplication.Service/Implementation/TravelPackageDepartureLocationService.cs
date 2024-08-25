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
    public class TravelPackageDepartureLocationService: ITravelPackageDepartureLocationService
    {
        private readonly IRepository<TravelPackageDepartureLocation> _travelPackageRepository;

        public TravelPackageDepartureLocationService(IRepository<TravelPackageDepartureLocation> travelPackageRepository)
        {
            _travelPackageRepository = travelPackageRepository;
        }

        public void CreateNewTravelPackageDepartureLocation(TravelPackageDepartureLocation p)
        {
            _travelPackageRepository.Insert(p);
        }

        public void DeleteTravelPackageDepartureLocation(Guid? id)
        {
            TravelPackageDepartureLocation tp = _travelPackageRepository.Get(id);
            if (tp != null)
            {
                _travelPackageRepository.Delete(tp);
            }
        }
  
        public List<TravelPackageDepartureLocation> GetAllTravelPackageDepartureLocations()
        {
            return _travelPackageRepository.GetAll().ToList();
        }

        public List<TravelPackageDepartureLocation> GetAllTravelPackageItinerariesByIds(List<Guid> ids)
        {
            return _travelPackageRepository.GetAll().Where(i => ids.Contains(i.Id)).ToList();
        }


        public TravelPackageDepartureLocation GetDetailsForTravelPackageDepartureLocation(Guid? id)
        {
            return _travelPackageRepository.Get(id);
        }

        public void UpdateExistingTravelPackageDepartureLocation(TravelPackageDepartureLocation p)
        {
            _travelPackageRepository.Update(p);
        }
    }
}
