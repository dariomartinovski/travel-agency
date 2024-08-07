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
    public class DepartureLocationService : IDepartureLocationService
    {
        private readonly IDepartureLocationRepository _departureLocationRepository;
        public DepartureLocationService(IDepartureLocationRepository departureLocationRepository)
        {
            _departureLocationRepository = departureLocationRepository;
        }

        public void CreateNewDepartureLocation(DepartureLocation p)
        {
            _departureLocationRepository.Insert(p);
        }

        public void DeleteDepartureLocation(Guid id)
        {
            var location = _departureLocationRepository.Get(id);
            if (location != null) { 
                _departureLocationRepository.Delete(location);
            }
        }

        public List<DepartureLocation> GetAllDepartureLocations()
        {
            return _departureLocationRepository.GetAll().ToList();
        }

        public DepartureLocation GetDetailsForDepartureLocation(Guid? id)
        {
            return _departureLocationRepository.Get(id);
        }

        public void UpdateExistingDepartureLocation(DepartureLocation p)
        {
            _departureLocationRepository.Update(p);
        }
    }
}
