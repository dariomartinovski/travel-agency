using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Model;

namespace TravelAgencyApplication.Service.Interface
{
    public interface ITravelPackageTagService
    {
        List<TravelPackageTag> GetAllTravelPackageTags();
        List<TravelPackageTag> GetAllTravelPackageItinerariesByIds(List<Guid> ids);
        TravelPackageTag GetDetailsForTravelPackageTag(Guid? id);
        void CreateNewTravelPackageTag(TravelPackageTag p);
        void UpdateExistingTravelPackageTag(TravelPackageTag p);
        void DeleteTravelPackageTag(Guid? id);
    }
}
