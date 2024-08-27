using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.DTO;
using TravelAgencyApplication.Domain.Model;

namespace TravelAgencyApplication.Service.Interface
{
    public interface IReservationService
    {
        List<Reservation> GetAllReservations();
        Reservation GetDetailsForReservation(Guid? id);
        void CreateNewReservation(Reservation p);
        void UpdateExistingReservation(Reservation p);
        void DeleteReservation(Guid id);
        ReservationDTO GetDetails(BaseEntity id);
    }
}
