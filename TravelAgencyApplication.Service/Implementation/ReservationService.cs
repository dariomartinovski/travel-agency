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
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public void CreateNewReservation(Reservation p)
        {
            _reservationRepository.Insert(p);
        }

        public void DeleteReservation(Guid id)
        {
            var reservation = _reservationRepository.Get(id);
            if (reservation != null)
            {
                _reservationRepository.Delete(reservation);
            }
        }

        public List<Reservation> GetAllReservations()
        {
            return _reservationRepository.GetAll().ToList();
        }

        public Reservation GetDetailsForReservation(Guid? id)
        {
            return _reservationRepository.Get(id);
        }

        public void UpdateExistingReservation(Reservation p)
        {
            _reservationRepository.Update(p);
        }
    }
}
