using Microsoft.EntityFrameworkCore;
using Stripe.Climate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.DTO;
using TravelAgencyApplication.Domain.Model;
using TravelAgencyApplication.Repository.Interface;
using TravelAgencyApplication.Web.Data;

namespace TravelAgencyApplication.Repository.Implementation
{
    public class ReservationRepository: IReservationRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Reservation> entities;
        string errorMessage = string.Empty;

        public ReservationRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Reservation>();
        }
        public IEnumerable<Reservation> GetAll()
        {
            return entities
                //.Include(t => t.User)
                //.Include(t => t.TravelPackage)
                .ToList();
        }

        public Reservation Get(Guid? id)
        {
            return entities
                .Include(t => t.User)
                .Include(t => t.TravelPackage)
                .Include("TravelPackage.Itineraries")
                .SingleOrDefaultAsync(s => s.Id == id).Result;
        }
      
        public ReservationDTO GetDetailsForReservation(Guid id)
        {
            var reservation = entities
                .Include(z=> z.User)
                .Include(z => z.TravelPackage)
                .Include("TravelPackage.Itineraries")
                .Include("TravelPackage.Itineraries.Itinerary")
                .SingleOrDefaultAsync(z => z.Id == id)
                .Result;

            if (reservation == null) 
                throw new ArgumentNullException("reservation");

            return new ReservationDTO
            {
                Id = reservation.Id,
                UserName = reservation.User.UserName,
                TravelPackageTitle = reservation.TravelPackage.Title,
                ItineraryTitles = reservation.TravelPackage.Itineraries.Select(i => i.Itinerary?.Title),
                NumberOfPeople = reservation.NumberOfPeople,
                HasPaid = reservation.HasPaid,
                TotalPrice = reservation.TotalPrice
            };
        }

        public void Insert(Reservation entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(Reservation entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(Reservation entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
