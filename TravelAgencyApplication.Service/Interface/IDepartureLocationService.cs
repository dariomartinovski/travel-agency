using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Model;

namespace TravelAgencyApplication.Service.Interface
{
    public interface IDepartureLocationService
    {
        List<DepartureLocation> GetAllDepartureLocations();
        List<DepartureLocation> GetAllDepartureLocationsByIds(List<Guid> ids);
        DepartureLocation GetDetailsForDepartureLocation(Guid? id);
        void CreateNewDepartureLocation(DepartureLocation p);
        void UpdateExistingDepartureLocation(DepartureLocation p);
        void DeleteDepartureLocation(Guid id);
    }
}
