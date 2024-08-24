using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Model;

namespace TravelAgencyApplication.Service.Interface
{
    public interface ITravelPackageDepartureLocationService
    {
        List<TravelPackageDepartureLocation> GetAllTravelPackageDepartureLocations();
        List<TravelPackageDepartureLocation> GetAllTravelPackageItinerariesByIds(List<Guid> ids);
        TravelPackageDepartureLocation GetDetailsForTravelPackageDepartureLocation(Guid? id);
        void CreateNewTravelPackageDepartureLocation(TravelPackageDepartureLocation p);
        void UpdateExistingTravelPackageDepartureLocation(TravelPackageDepartureLocation p);
        void DeleteTravelPackageDepartureLocation(Guid? id);
    }
}
