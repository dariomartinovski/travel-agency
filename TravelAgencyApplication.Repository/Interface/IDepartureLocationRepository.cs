using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Model;

namespace TravelAgencyApplication.Repository.Interface
{
    public interface IDepartureLocationRepository
    {
        IEnumerable<DepartureLocation> GetAll();
        IEnumerable<DepartureLocation> GetAllDepartureLocationsByIds(List<Guid> ids);
        DepartureLocation Get(Guid? id);
        void Insert(DepartureLocation entity);
        void Update(DepartureLocation entity);
        void Delete(DepartureLocation entity);
    }
}
