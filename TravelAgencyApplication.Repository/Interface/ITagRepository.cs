using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Identity;
using TravelAgencyApplication.Domain.Model;

namespace TravelAgencyApplication.Repository.Interface
{
    public interface ITagRepository
    {
        IEnumerable<Tag> GetAll();
        Tag Get(Guid id);
        void Insert(Tag entity);
        void Update(Tag entity);
        void Delete(Tag entity);
    }
}
