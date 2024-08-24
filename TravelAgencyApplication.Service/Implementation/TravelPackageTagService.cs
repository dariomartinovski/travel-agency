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
    public class TravelPackageTagService: ITravelPackageTagService
    {
        private readonly IRepository<TravelPackageTag> _travelPackageTagRepository;

        public TravelPackageTagService(IRepository<TravelPackageTag> travelPackageTagRepository)
        {
            _travelPackageTagRepository = travelPackageTagRepository;
        }

        public void CreateNewTravelPackageTag(TravelPackageTag p)
        {
            _travelPackageTagRepository.Insert(p);
        }

        public void DeleteTravelPackageTag(Guid? id)
        {
            TravelPackageTag tp = _travelPackageTagRepository.Get(id);
            if (tp != null)
            {
                _travelPackageTagRepository.Delete(tp);
            }
        }

        public List<TravelPackageTag> GetAllTravelPackageTags()
        {
            return _travelPackageTagRepository.GetAll().ToList();
        }

        public List<TravelPackageTag> GetAllTravelPackageItinerariesByIds(List<Guid> ids)
        {
            return _travelPackageTagRepository.GetAll().Where(i => ids.Contains(i.Id)).ToList();
        }


        public TravelPackageTag GetDetailsForTravelPackageTag(Guid? id)
        {
            return _travelPackageTagRepository.Get(id);
        }

        public void UpdateExistingTravelPackageTag(TravelPackageTag p)
        {
            _travelPackageTagRepository.Update(p);
        }
    }
}
