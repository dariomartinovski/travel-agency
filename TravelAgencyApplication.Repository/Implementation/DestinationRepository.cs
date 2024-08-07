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
    public class DestinationRepository: IDestinationRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Destination> entities;
        string errorMessage = string.Empty;

        public DestinationRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Destination>();
        }
        public IEnumerable<Destination> GetAll()
        {
            return entities.Include(t => t.City)
                .Include(t => t.Country)
                .ToList();
        }

        public Destination Get(Guid? id)
        {
            return entities
                .Include(t => t.City)
                .Include(t => t.Country)
                .SingleOrDefault(s => s.Id == id);
        }
        public void Insert(Destination entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(Destination entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(Destination entity)
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
