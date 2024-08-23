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
    public class CityService: ICityService
    {
        private readonly IRepository<City> _cityRepository;
        public CityService(IRepository<City> countryRepository)
        {
            _cityRepository = countryRepository;
        }

        public void CreateNewCity(City p)
        {
            _cityRepository.Insert(p);
        }

        public void DeleteCity(Guid id)
        {
            var destination = _cityRepository.Get(id);
            if (destination != null)
            {
                _cityRepository.Delete(destination);
            }
        }

        public List<City> GetAllCities()
        {
            return _cityRepository.GetAll().ToList();
        }

        public City GetDetailsForCity(Guid? id)
        {
            return _cityRepository.Get(id);
        }

        public void UpdateExistingCity(City p)
        {
            _cityRepository.Update(p);
        }
    }
}
