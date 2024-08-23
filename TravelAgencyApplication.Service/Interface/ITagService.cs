using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Model;

namespace TravelAgencyApplication.Service.Interface
{
    public interface ITagService
    {
        List<Tag> GetAllTags();
        Tag GetDetailsForTag(Guid? id);
        List<Tag> GetAllTagsByIds(List<Guid> tagIds);
        void CreateNewTag(Tag p);
        void UpdateExistingTag(Tag p);
        void DeleteTag(Guid id);
    }
}
