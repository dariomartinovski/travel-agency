using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Model;

namespace TravelAgencyApplication.Service.Interface
{
    public interface ICountryService
    {
        List<Country> GetAllCountries();
        Country GetDetailsForCountry(Guid? id);
        void CreateNewCountry(Country p);
        void UpdateExistingCountry(Country p);
        void DeleteCountry(Guid id);
    }
}
