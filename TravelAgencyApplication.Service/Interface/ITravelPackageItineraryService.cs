using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Model;

namespace TravelAgencyApplication.Service.Interface
{
    public interface ITravelPackageItineraryService
    {
        List<TravelPackageItinerary> GetAllTravelPackageItinerarys();
        TravelPackageItinerary GetDetailsForTravelPackageItinerary(Guid? id);
        void CreateNewTravelPackageItinerary(TravelPackageItinerary p);
        void UpdateExistingTravelPackageItinerary(TravelPackageItinerary p);
        void DeleteTravelPackageItinerary(Guid? id);
    }
}
