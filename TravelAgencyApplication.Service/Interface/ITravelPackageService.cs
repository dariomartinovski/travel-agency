using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Model;

namespace TravelAgencyApplication.Service.Interface
{
    public interface ITravelPackageService
    {
        List<TravelPackage> GetAllTravelPackages();
        TravelPackage GetDetailsForTravelPackage(Guid? id);
        void CreateNewTravelPackage(TravelPackage p);
        void UpdateExistingTravelPackage(TravelPackage p);
        void DeleteTravelPackage(Guid? id);
    }
}
