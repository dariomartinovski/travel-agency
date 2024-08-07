using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Model;

namespace TravelAgencyApplication.Service.Interface
{
    public interface IDestinationService
    {
        List<Destination> GetAllDestinations();
        Destination GetDetailsForDestination(Guid? id);
        void CreateNewDestination(Destination p);
        void UpdateExistingDestination(Destination p);
        void DeleteDestination(Guid id);
    }
}
