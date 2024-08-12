using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Model;

namespace TravelAgencyApplication.Repository.Interface
{
    public interface IItineraryRepository
    {
        IEnumerable<Itinerary> GetAll();
        IEnumerable<Itinerary> GetAllItinerariesByIds(List<Guid> itineraries);
        Itinerary Get(Guid? id);
        void Insert(Itinerary entity);
        void Update(Itinerary entity);
        void Delete(Itinerary entity);
    }
}
