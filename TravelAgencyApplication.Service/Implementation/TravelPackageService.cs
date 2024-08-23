using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Model;
using TravelAgencyApplication.Repository.Implementation;
using TravelAgencyApplication.Repository.Interface;
using TravelAgencyApplication.Service.Interface;

namespace TravelAgencyApplication.Service.Implementation
{
    public class TravelPackageService : ITravelPackageService
    {
        private readonly ITravelPackageRepository _travelPackageRepository;

        public TravelPackageService(ITravelPackageRepository travelPackageRepository)
        {
            _travelPackageRepository = travelPackageRepository;
        }

        public void CreateNewTravelPackage(TravelPackage p)
        {
            
            _travelPackageRepository.Insert(p);
        }

        public void DeleteTravelPackage(Guid? id)
        {
            var travelPackage = _travelPackageRepository.Get(id);
            _travelPackageRepository.Delete(travelPackage);
        }

        public List<TravelPackage> GetAllTravelPackages()
        {
           return _travelPackageRepository.GetAll().ToList();
        }

        public TravelPackage GetDetailsForTravelPackage(Guid? id)
        {
            return _travelPackageRepository.Get(id);
        }

        public void UpdateExistingTravelPackage(TravelPackage p)
        {
            _travelPackageRepository.Update(p);
        }
    }
}
