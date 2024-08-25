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
    public class DepartureLocationRepository : IDepartureLocationRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<DepartureLocation> entities;
        string errorMessage = string.Empty;

        public DepartureLocationRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<DepartureLocation>();
        }

        public IEnumerable<DepartureLocation> GetAll()
        {
            return entities.Include(t => t.City)
                .Include(t => t.TravelPackages)
                .Include("TravelPackages.TravelPackage")
                .ToList();
        }
        public IEnumerable<DepartureLocation> GetAllDepartureLocationsByIds(List<Guid> ids)
        {
            return entities
                .Include(t => t.City)
                .Include(t => t.TravelPackages)
                .Include("TravelPackages.TravelPackage")
                .Where(it => ids.Contains(it.Id))
                .ToList();
        }

        public DepartureLocation Get(Guid? id)
        {
            return entities
                .Include(t => t.City)
                .Include(t => t.TravelPackages)
                .Include("TravelPackages.TravelPackage")
                .SingleOrDefault(s => s.Id == id);
        }
        public void Insert(DepartureLocation entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(DepartureLocation entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(DepartureLocation entity)
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
