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
                .Include(t=>t.DepartureLocations)
                .Include(t=>t.Destination)
                .Include(t=>t.Guide).ToList();
        }

        public TravelPackage Get(Guid? id)
        {
            return entities
                .Include(t => t.DepartureLocations)
                .Include(t => t.Destination)
                .Include(t => t.Guide)
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
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(TravelPackage entity)
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
