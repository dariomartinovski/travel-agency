using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.DTO;
using TravelAgencyApplication.Domain.Model;
using TravelAgencyApplication.Repository.Interface;
using TravelAgencyApplication.Web.Data;

namespace TravelAgencyApplication.Repository.Implementation
{
    public class TravelPackageRepository : ITravelPackageRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<TravelPackage> entities;
        string errorMessage = string.Empty;

        public TravelPackageRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<TravelPackage>();
        }
        public IEnumerable<TravelPackage> GetAll()
        {
            return entities.Include(t=>t.Itineraries)
                .Include(t => t.Destination)
                .Include(t => t.Destination.Country)
                .Include(t => t.Destination.City)
                .Include(t => t.Itineraries)
                .Include("Itineraries.Itinerary")
                .Include(t => t.DepartureLocations)
                .Include("DepartureLocations.DepartureLocation")
                .Include(t => t.Tags)
                .Include("Tags.Tag")
                .Include(t => t.Guide).ToList();
        }

        public TravelPackage Get(Guid? id)
        {
            return entities
                .Include(t => t.Destination)
                .Include(t => t.Destination.Country)
                .Include(t => t.Destination.City)
                .Include(t => t.Guide)
                .Include(t => t.Itineraries)
                .Include("Itineraries.Itinerary")
                .Include(t => t.DepartureLocations)
                .Include("DepartureLocations.DepartureLocation")
                .Include(t => t.Tags)
                .Include("Tags.Tag")
                .SingleOrDefault(s => s.Id == id);
        }
        public void Insert(TravelPackage entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(TravelPackage entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var existingEntity = context.TravelPackages.Local.SingleOrDefault(e => e.Id == entity.Id);
            if (existingEntity != null)
            {
                context.Entry(existingEntity).State = EntityState.Detached;
            }

            entities.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;

            context.SaveChanges();
        }

        public void Delete(TravelPackage entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            // Now remove the main entity
            context.TravelPackages.Remove(entity);
            context.SaveChanges();
        }

    }
}
