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
    public class DestinationService : IDestinationService
    {
        private readonly IDestinationRepository _destinationRepository;
        public DestinationService(IDestinationRepository destinationRepository)
        {
            _destinationRepository = destinationRepository;
        }

        public void CreateNewDestination(Destination p)
        {
            _destinationRepository.Insert(p);
        }

        public void DeleteDestination(Guid id)
        {
            var destination = _destinationRepository.Get(id);
            if (destination != null)
            {
                _destinationRepository.Delete(destination);
            }
        }

        public List<Destination> GetAllDestinations()
        {
            return _destinationRepository.GetAll().ToList();
        }

        public Destination GetDetailsForDestination(Guid? id)
        {
            return _destinationRepository.Get(id);
        }

        public void UpdateExistingDestination(Destination p)
        {
            _destinationRepository.Update(p);
        }
    }
}
