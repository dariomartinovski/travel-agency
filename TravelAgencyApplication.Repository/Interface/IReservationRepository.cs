using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.DTO;
using TravelAgencyApplication.Domain.Model;

namespace TravelAgencyApplication.Repository.Interface
{
    public interface IReservationRepository
    {
        IEnumerable<Reservation> GetAll();
        Reservation Get(Guid? id);
        void Insert(Reservation entity);
        void Update(Reservation entity);
        void Delete(Reservation entity);
        ReservationDTO GetDetailsForReservation(Guid id);

    }
}
