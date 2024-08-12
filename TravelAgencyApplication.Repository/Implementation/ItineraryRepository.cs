using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Model;
using TravelAgencyApplication.Repository.Interface;
using TravelAgencyApplication.Web.Data;

namespace TravelAgencyApplication.Repository.Implementation
{
    public class ItineraryRepository : IItineraryRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Itinerary> entities;
        string errorMessage = string.Empty;

        public ItineraryRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Itinerary>();
        }
        public IEnumerable<Itinerary> GetAll()
        {
            return entities
                .Include(t => t.Destination)
                .Include(t => t.TravelPackages)
                .ToList();
        }
        public IEnumerable<Itinerary> GetAllItinerariesByIds(List<Guid> iteneraries)
        {
            return entities
                .Include(t => t.Destination)
                .Include(t => t.TravelPackages)
                .Where(t => iteneraries.Contains(t.Id))
                .ToList();
        }

        public Itinerary Get(Guid? id)
        {
            return entities
                .Include(t => t.Destination)
                .Include(t => t.TravelPackages)
                .SingleOrDefault(s => s.Id == id);
        }
        public void Insert(Itinerary entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(Itinerary entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(Itinerary entity)
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
