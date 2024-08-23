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
    public class CountryService: ICountryService
    {
        private readonly IRepository<Country> _countryRepository;
        public CountryService(IRepository<Country> countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public void CreateNewCountry(Country p)
        {
            _countryRepository.Insert(p);
        }

        public void DeleteCountry(Guid id)
        {
            var destination = _countryRepository.Get(id);
            if (destination != null)
            {
                _countryRepository.Delete(destination);
            }
        }

        public List<Country> GetAllCountries()
        {
            return _countryRepository.GetAll().ToList();
        }

        public Country GetDetailsForCountry(Guid? id)
        {
            return _countryRepository.Get(id);
        }

        public void UpdateExistingCountry(Country p)
        {
            _countryRepository.Update(p);
        }
    }
}
