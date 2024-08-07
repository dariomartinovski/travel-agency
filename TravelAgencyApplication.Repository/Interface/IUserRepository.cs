using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Identity;
using TravelAgencyApplication.Domain.Model;

namespace TravelAgencyApplication.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<TAUser> GetAll();
        TAUser Get(string id);
        void Insert(TAUser entity);
        void Update(TAUser entity);
        void Delete(TAUser entity);
    }
}
