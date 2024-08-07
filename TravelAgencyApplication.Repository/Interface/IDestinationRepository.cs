using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Model;

namespace TravelAgencyApplication.Repository.Interface
{
    public interface IDestinationRepository
    {
        IEnumerable<Destination> GetAll();
        Destination Get(Guid? id);
        void Insert(Destination entity);
        void Update(Destination entity);
        void Delete(Destination entity);
    }
}
