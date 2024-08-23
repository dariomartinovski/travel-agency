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
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public List<Tag> GetAllTags()
        {
            return _tagRepository.GetAll().ToList();
        }
        
        public List<Tag> GetAllTagsByIds(List<Guid> tags)
        {
            return _tagRepository.GetAllTagsByIds(tags).ToList();
        }

        public Tag GetDetailsForTag(Guid? id)
        {
            return _tagRepository.Get(id);
        }

        public void CreateNewTag(Tag p)
        {
            _tagRepository.Insert(p);
        }

        public void DeleteTag(Guid id)
        {
            var tag = _tagRepository.Get(id);
            if (tag != null)
            {
                _tagRepository.Delete(tag);
            }
        }

        public void UpdateExistingTag(Tag p)
        {
            _tagRepository.Update(p);
        }
    }
}
