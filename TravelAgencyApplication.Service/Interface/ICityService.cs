using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Model;

namespace TravelAgencyApplication.Service.Interface
{
    public interface ICityService
    {
        List<City> GetAllCities();
        City GetDetailsForCity(Guid? id);
        void CreateNewCity(City p);
        void UpdateExistingCity(City p);
        void DeleteCity(Guid id);
    }
}
