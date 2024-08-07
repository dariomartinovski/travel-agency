using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Model;

namespace TravelAgencyApplication.Service.Interface
{
    public interface IItineraryService
    {
        List<Itinerary> GetAllItinerarys();
        Itinerary GetDetailsForItinerary(Guid? id);
        void CreateNewItinerary(Itinerary p);
        void UpdateExistingItinerary(Itinerary p);
        void DeleteItinerary(Guid id);
    }
}
