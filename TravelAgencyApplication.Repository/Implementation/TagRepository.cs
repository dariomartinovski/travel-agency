using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Identity;
using TravelAgencyApplication.Domain.Model;
using TravelAgencyApplication.Repository.Interface;
using TravelAgencyApplication.Web.Data;

namespace TravelAgencyApplication.Repository.Implementation
{
    public class TagRepository: ITagRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Tag> entities;
        string errorMessage = string.Empty;

        public TagRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Tag>();
        }
        public IEnumerable<Tag> GetAll()
        {
            return entities.Include(t => t.TravelPackages).ToList();
        }
        
        public IEnumerable<Tag> GetAllTagsByIds(List<Guid> tags)
        {
            return entities.Include(t => t.TravelPackages)
                .Where(t => tags.Contains(t.Id))
                .ToList();
        }

        public Tag Get(Guid? id)
        {
            return entities
                .Include(t => t.TravelPackages)
                .SingleOrDefault(s => s.Id == id);
        }
        public void Insert(Tag entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(Tag entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(Tag entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.Entry(entity).Collection(e => e.TravelPackages).Load();

            // Manually remove related entities
            context.TravelPackageTags.RemoveRange(entity.TravelPackages);

            context.Tags.Remove(entity);
            context.SaveChanges();
        }
    }
}
