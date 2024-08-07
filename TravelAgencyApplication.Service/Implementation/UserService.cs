using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Identity;
using TravelAgencyApplication.Repository.Interface;
using TravelAgencyApplication.Service.Interface;

namespace TravelAgencyApplication.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void CreateNewTAUser(TAUser p)
        {
            _userRepository.Insert(p);
        }

        public void DeleteTAUser(string id)
        {
            var user = _userRepository.Get(id);
            _userRepository.Delete(user);
        }

        public List<TAUser> GetAllTAUsers()
        {
            return _userRepository.GetAll().ToList();
        }

        public TAUser GetDetailsForTAUser(string id)
        {
            return _userRepository.Get(id);
        }

        public void UpdateExistingTAUser(TAUser p)
        {
            _userRepository.Update(p);
        }
    }
}
